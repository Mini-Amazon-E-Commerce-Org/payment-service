using Microsoft.EntityFrameworkCore;
using PaymentServiceApi.Data;
using PaymentServiceApi.Models;


namespace PaymentServiceApi.Repositories
{
    public class PaymentRepository : IPaymentRepo
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            return await _context.Payments
                .Where(p => p.OrderId == orderId)
                .OrderByDescending(p => p.TransactionId)
                .FirstOrDefaultAsync();
        }
    }
}
