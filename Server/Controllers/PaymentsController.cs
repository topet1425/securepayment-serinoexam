using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePayment.Shared.Models;

namespace SecurePayment.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = "CanSubmitPayments")]
        public async Task<ActionResult<PaymentResponse>> Create([FromBody] PaymentRequest request)
        {
            // helps show loading state in UI
            await Task.Delay(1000);

            // optional: simulate server error
            if (request.ReferenceId.StartsWith("500", StringComparison.OrdinalIgnoreCase))
                return StatusCode(500);

            return Ok(new PaymentResponse
            {
                PaymentId = Guid.NewGuid().ToString("N"),
                ReferenceId = request.ReferenceId,
                Status = "Accepted"
            });
        }
    }
}
