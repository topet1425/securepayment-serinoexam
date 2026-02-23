namespace SecurePayment.Shared.Models
{
    public class PaymentResponse
    {
        public string PaymentId { get; set; } = "";
        public string ReferenceId { get; set; } = "";
        public string Status { get; set; } = "Accepted";
    }

}