using PaymentServiceApi.Models;
using PaymentServiceApi.Repositories;
using PaymentServiceApi.DTOs;
using PaymentServiceApi.Enums;

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
                if(existingPayment.Status == PaymentStatus.Success)
                {
                    return new PaymentResponseDto
                    {
                        Success = false,
                        Message = "Payment already exists for this order"
                    };
                }

                if(existingPayment.Status == PaymentStatus.Pending)
                {
                    return new PaymentResponseDto
                    {
                        Success = false,
                        Message = "Payment is already in Progress"
                    };
                }

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

            PaymentStatus status;

            if (request.Amount % 2 == 0)
                status = PaymentStatus.Success;
            else
                status = PaymentStatus.Failed;

            // Mock processing
            var payment = new Payment
            {
                OrderId = request.OrderId,
                Amount = request.Amount,
                Status = status,
                PaymentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            var saved = await _repository.AddPaymentAsync(payment);

            if (status == PaymentStatus.Failed)
            {
                return new PaymentResponseDto
                {
                    Success = false,
                    Message = "Payment failed due to mock rule",
                    TransactionId = saved.TransactionId
                };
            }

            return new PaymentResponseDto
            {
                Success = true,
                Message = "Payment processed successfully",
                TransactionId = saved.TransactionId
            };

        }
    }
}
