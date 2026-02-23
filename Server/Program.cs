using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication;
using SecurePayment.Server.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = AuthHandler.SchemeName;
        options.DefaultChallengeScheme = AuthHandler.SchemeName;
    })
    .AddScheme<AuthenticationSchemeOptions, AuthHandler>(
        AuthHandler.SchemeName, _ => { });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanSubmitPayments", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("permission", Permissions.PaymentsWrite);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
