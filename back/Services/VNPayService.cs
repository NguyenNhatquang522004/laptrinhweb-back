using backapi.Configuration;
using backapi.Model;
using backapi.Repository;
using backapi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static backapi.Enums.enums;

namespace backapi.Services
{
    public class VNPayService : IVNPayRepository
    {
        private readonly VNPayConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VNPayService> _logger;

        public VNPayService(
            IOptions<VNPayConfiguration> config,
            ApplicationDbContext context,
            ILogger<VNPayService> logger)
        {
            _config = config.Value;
            _context = context;
            _logger = logger;
        }

        public async Task<VNPayResponse> CreatePaymentUrlAsync(VNPayRequest request)
        {
            try
            {
                // Tạo OrderId unique
                var orderId = DateTime.Now.Ticks.ToString();

                // Tạo payment record
                var payment = new PaymentHistory
                {
                    UserId = request.UserId,
                    SubscriptionId = request.SubscriptionId,
                    Amount = request.Amount,
                    Currency = "VND",
                    PaymentMethod = "VNPay",
                    TransactionId = orderId,
                    Status = PaymentStatus.Pending,
                    Description = request.Description,
                    Gateway = "VNPay"
                };

                _context.PaymentHistories.Add(payment);
                await _context.SaveChangesAsync();

                // Tạo VNPay parameters
                var vnpayData = new SortedDictionary<string, string>
                {
                    {"vnp_Version", _config.Version},
                    {"vnp_Command", _config.Command},
                    {"vnp_TmnCode", _config.TmnCode},
                    {"vnp_Amount", ((int)(request.Amount * 100)).ToString()}, // VNPay yêu cầu số tiền * 100
                    {"vnp_CurrCode", _config.CurrCode},
                    {"vnp_TxnRef", orderId},
                    {"vnp_OrderInfo", request.OrderInfo},
                    {"vnp_OrderType", "other"},
                    {"vnp_Locale", _config.Locale},
                    {"vnp_ReturnUrl", request.ReturnUrl},
                    {"vnp_IpnUrl", _config.IpnUrl},
                    {"vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")}
                };

                // Tạo query string
                var query = string.Join("&", vnpayData.Select(x => $"{x.Key}={HttpUtility.UrlEncode(x.Value)}"));

                // Tạo secure hash
                var secureHash = CreateSecureHash(query);

                var paymentUrl = $"{_config.BaseUrl}?{query}&vnp_SecureHash={secureHash}";

                return new VNPayResponse
                {
                    Success = true,
                    PaymentUrl = paymentUrl,
                    OrderId = orderId,
                    Message = "Payment URL created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating VNPay payment URL");
                return new VNPayResponse
                {
                    Success = false,
                    Message = "Error creating payment URL"
                };
            }
        }

        public async Task<bool> ValidateCallbackAsync(VNPayCallback callback)
        {
            try
            {
                // Tạo query string để validate
                var vnpayData = new SortedDictionary<string, string>
                {
                    {"vnp_Amount", callback.vnp_Amount},
                    {"vnp_BankCode", callback.vnp_BankCode},
                    {"vnp_BankTranNo", callback.vnp_BankTranNo},
                    {"vnp_CardType", callback.vnp_CardType},
                    {"vnp_OrderInfo", callback.vnp_OrderInfo},
                    {"vnp_PayDate", callback.vnp_PayDate},
                    {"vnp_ResponseCode", callback.vnp_ResponseCode},
                    {"vnp_TmnCode", callback.vnp_TmnCode},
                    {"vnp_TransactionNo", callback.vnp_TransactionNo},
                    {"vnp_TransactionStatus", callback.vnp_TransactionStatus},
                    {"vnp_TxnRef", callback.vnp_TxnRef}
                };

                var query = string.Join("&", vnpayData.Select(x => $"{x.Key}={x.Value}"));
                var secureHash = CreateSecureHash(query);

                return secureHash.Equals(callback.vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating VNPay callback");
                return false;
            }
        }

        public async Task<PaymentHistory> ProcessPaymentCallbackAsync(VNPayCallback callback)
        {
            // Tìm payment record
            var payment = await _context.PaymentHistories
                .Include(p => p.User)
                .Include(p => p.Subscription)
                .FirstOrDefaultAsync(p => p.TransactionId == callback.vnp_TxnRef);

            if (payment == null)
            {
                throw new InvalidOperationException("Payment not found");
            }

            // Cập nhật payment status
            payment.GatewayTransactionId = callback.vnp_TransactionNo;
            payment.GatewayResponse = $"ResponseCode: {callback.vnp_ResponseCode}, TransactionStatus: {callback.vnp_TransactionStatus}";
            payment.ProcessedAt = DateTime.UtcNow;
            payment.UpdatedAt = DateTime.UtcNow;

            if (callback.vnp_ResponseCode == "00" && callback.vnp_TransactionStatus == "00")
            {
                payment.Status = PaymentStatus.Completed;

                // Cập nhật subscription nếu có
                if (payment.Subscription != null)
                {
                    payment.Subscription.Status = SubscriptionStatus.Active;
                    payment.Subscription.UpdatedAt = DateTime.UtcNow;

                    // Cập nhật user premium status
                    var user = await _context.Users.FindAsync(payment.UserId);
                    if (user != null)
                    {
                        user.IsPremium = true;
                        user.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
                payment.FailureReason = $"VNPay error: {callback.vnp_ResponseCode}";
            }

            await _context.SaveChangesAsync();
            return payment;
        }

        private string CreateSecureHash(string data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_config.HashSecret);
            var dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}

