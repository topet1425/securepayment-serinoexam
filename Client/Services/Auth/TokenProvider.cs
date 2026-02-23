namespace SecurePayment.Client.Services.Auth;
public class TokenProvider : ITokenProvider
{
    private string _token = "valid-token"; // in-memory demo

    public Task<string> GetAccessTokenAsync(CancellationToken ct = default)
        => Task.FromResult(_token);

    public Task SetTokenAsync(string token, CancellationToken ct = default)
    {
        _token = token;
        return Task.CompletedTask;
    }
}