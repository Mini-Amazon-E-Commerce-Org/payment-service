using System.ComponentModel.DataAnnotations;
namespace PaymentServiceApi.DTOs
{
    public class PaymentRequestDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Amount must be graeter than 0")]
        public decimal Amount { get; set; }
    }
}
