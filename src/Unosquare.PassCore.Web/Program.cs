using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Unosquare.PassCore.Web.Models;
using Unosquare.PassCore.PasswordProvider;

#if DEBUG
using Unosquare.PassCore.Web.Helpers;
#elif PASSCORE_LDAP_PROVIDER
using Zyborg.PassCore.PasswordProvider.LDAP;
using Microsoft.Extensions.Logging;
#else
using Unosquare.PassCore.PasswordProvider;
#endif

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

// Register services
builder.Services.Configure<ClientSettings>(configuration.GetSection(nameof(ClientSettings)));
builder.Services.Configure<WebSettings>(configuration.GetSection(nameof(WebSettings)));

#if DEBUG
builder.Services.Configure<PasswordChangeOptions>(configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IPasswordChangeProvider, DebugPasswordChangeProvider>();
#elif PASSCORE_LDAP_PROVIDER
builder.Services.Configure<LdapPasswordChangeOptions>(configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IPasswordChangeProvider, LdapPasswordChangeProvider>();
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton(typeof(ILogger), sp =>
{
    var loggerFactory = sp.GetService<ILoggerFactory>();
    return loggerFactory.CreateLogger("PassCoreLDAPProvider");
});
#else
builder.Services.Configure<PasswordChangeOptions>(configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IPasswordChangeProvider, PasswordChangeProvider>();
#endif

builder.Services.AddControllers();

var app = builder.Build();

// Get WebSettings from DI
var webSettings = app.Services.GetRequiredService<IOptions<WebSettings>>().Value;

if (webSettings.EnableHttpsRedirect)
{
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
