using PaymentServiceApi.DTOs;

namespace PaymentServiceApi.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request);
    }
}
