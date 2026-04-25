using PaymentServiceApi.Models;
using PaymentServiceApi.Repositories;
using PaymentServiceApi.DTOs;

namespace PaymentServiceApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _repository;

        public PaymentService(IPaymentRepo repository)
        {
            _repository = repository;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto request)
        {
            // Validation 1: Check duplicate payment
            var existingPayment = await _repository.GetByOrderIdAsync(request.OrderId);
            if (existingPayment != null)
            {
                return new PaymentResponseDto
                {
                    Success = false,
                    Message = "Payment already exists for this order"
                };
            }

            // Validation 2: Business rule (mock example)
            if (request.Amount <= 0 )
            {
                return new PaymentResponseDto
                {
                    Success = false,
                    Message = "Invalid Amount"
                };
            }

            // Mock processing
            var payment = new Payment
            {
                OrderId = request.OrderId,
                Amount = request.Amount,
                Status = "Success",
                PaymentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            var saved = await _repository.AddPaymentAsync(payment);

            return new PaymentResponseDto
            {
                Success = true,
                Message = "Payment processed successfully",
                TransactionId = saved.TransactionId
            };
        }
    }
}
