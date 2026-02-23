using System.Net;
using System.Net.Http.Json;
using SecurePayment.Shared.Models;
using SecurePayment.Client.Services.Auth;

namespace SecurePayment.Client.Services
{
    public class PaymentsApiClient
{
        private readonly HttpClient _http;
        private readonly ITokenProvider _tokenProvider;

        public PaymentsApiClient(HttpClient http, ITokenProvider tokenProvider)
        {
            _http = http;
            _tokenProvider = tokenProvider;
        }

        public async Task<PaymentResponse> SubmitAsync(PaymentRequest model, CancellationToken ct = default)
        {
            var token = await _tokenProvider.GetAccessTokenAsync(ct);

            using var req = new HttpRequestMessage(HttpMethod.Post, "api/payments");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // explicit mapping = avoids accidental over-posting
            req.Content = JsonContent.Create(new
            {
                amount = model.Amount,
                currency = model.Currency,
                referenceId = model.ReferenceId
            });

            using var res = await _http.SendAsync(req, ct);

            if (res.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (res.StatusCode == HttpStatusCode.Forbidden)
                throw new InvalidOperationException("Forbidden");

            if (!res.IsSuccessStatusCode)
                throw new HttpRequestException($"HTTP {(int)res.StatusCode}");

            return (await res.Content.ReadFromJsonAsync<PaymentResponse>(cancellationToken: ct))!;
        }

    }
}
