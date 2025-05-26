using backapi.Model;
using backapi.Settings;

namespace backapi.Repository
{
    public interface IVNPayRepository
    {
        Task<VNPayResponse> CreatePaymentUrlAsync(VNPayRequest request);
        Task<bool> ValidateCallbackAsync(VNPayCallback callback);
        Task<PaymentHistory> ProcessPaymentCallbackAsync(VNPayCallback callback);

    }
}
