namespace SecurePayment.Client.Services.Auth
{
    public interface ITokenProvider
{
        Task<string> GetAccessTokenAsync(CancellationToken ct = default);
        Task SetTokenAsync(string token, CancellationToken ct = default);
    }
}
