using System.ComponentModel.DataAnnotations;

namespace SecurePayment.Shared.Models
{
    public class PaymentRequest
    {
        [Required]
        [Range(0.01, 999999999)]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string ReferenceId { get; set; }
    }

}