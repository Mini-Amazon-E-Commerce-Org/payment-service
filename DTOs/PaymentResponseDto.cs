namespace PaymentServiceApi.DTOs
{
    public class PaymentResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? TransactionId { get; set; }
    }
}
