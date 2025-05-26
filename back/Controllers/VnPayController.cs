using backapi.Repository;
using backapi.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static backapi.Enums.enums;

namespace backapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVNPayRepository _vnPayService;
        private readonly ILogger<VnPayController> _logger;

        public VnPayController(IVNPayRepository vnPayService, ILogger<VnPayController> logger)
        {
            _vnPayService = vnPayService;
            _logger = logger;
        }

        [HttpPost("vnpay/create")]
        [Authorize]
        public async Task<IActionResult> CreateVNPayPayment([FromBody] VNPayRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _vnPayService.CreatePaymentUrlAsync(request);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating VNPay payment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("vnpay/callback")]
        public async Task<IActionResult> VNPayCallback([FromQuery] VNPayCallback callback)
        {
            try
            {
                // Validate callback
                var isValid = await _vnPayService.ValidateCallbackAsync(callback);
                if (!isValid)
                {
                    return BadRequest("Invalid callback signature");
                }

                // Process payment
                var payment = await _vnPayService.ProcessPaymentCallbackAsync(callback);

                // Redirect user based on payment status
                var redirectUrl = payment.Status == PaymentStatus.Completed
                    ? "/payment/success"
                    : "/payment/failed";

                return Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing VNPay callback");
                return Redirect("/payment/error");
            }
        }

        [HttpPost("vnpay/ipn")]
        public async Task<IActionResult> VNPayIPN([FromForm] VNPayCallback callback)
        {
            try
            {
                // Validate IPN
                var isValid = await _vnPayService.ValidateCallbackAsync(callback);
                if (!isValid)
                {
                    return BadRequest("00"); // VNPay yêu cầu trả về "00" khi thành công
                }

                // Process payment
                await _vnPayService.ProcessPaymentCallbackAsync(callback);

                return Ok("00"); // Thành công
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing VNPay IPN");
                return BadRequest("99"); // Lỗi
            }
        }
    }
}

