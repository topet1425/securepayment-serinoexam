using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace SecurePayment.Server.Auth
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "Bearer";

        public AuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
             ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var values))
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization header"));

            var header = values.ToString();
            if (!header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Invalid scheme"));

            var token = header["Bearer ".Length..].Trim();

            // Demo tokens:
            // valid-token -> authenticated + permission
            // no-permission-token -> authenticated, no permission -> 403
            // expired-token -> fail auth -> 401
            if (token == "expired-token")
                return Task.FromResult(AuthenticateResult.Fail("Expired token"));

            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult(AuthenticateResult.Fail("Empty token"));

            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "demo-user"),
            new(ClaimTypes.Name, "Demo User")
        };

            if (token == "valid-token")
                claims.Add(new Claim("permission", Permissions.PaymentsWrite));

            var identity = new ClaimsIdentity(claims, SchemeName);
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, SchemeName)));
        }
    }
}
