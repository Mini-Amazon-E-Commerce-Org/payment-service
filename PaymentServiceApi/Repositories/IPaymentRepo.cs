using PaymentServiceApi.Models;

namespace PaymentServiceApi.Repositories
{
    public interface IPaymentRepo
    {
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<Payment?> GetByOrderIdAsync(int orderId);
    }
}
