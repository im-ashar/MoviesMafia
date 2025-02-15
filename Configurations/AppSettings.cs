using System.Text.Json.Serialization;

namespace MoviesMafia.Configurations
{
    public static class AppSettings
    {
        public static Logging Logging { get; private set; }
        public static string AllowedHosts { get; private set; }
        public static string AutoEmbedUrl { get; private set; }
        public static ConnectionStrings ConnectionStrings { get; private set; }
        public static AdminDetails AdminDetails { get; private set; }
        public static SmtpConfig SmtpConfig { get; private set; }

        public static void SetupAppSettings(this IConfiguration configuration)
        {
            Logging = configuration.GetSection(nameof(Logging)).Get<Logging>() ?? throw new InvalidOperationException($"{nameof(Logging)} key is not found inside 'appsettings.json'.");
            AllowedHosts = configuration.GetSection(nameof(AllowedHosts)).Get<string>() ?? throw new InvalidOperationException($"{nameof(AllowedHosts)} key is not found inside 'appsettings.json'.");
            AutoEmbedUrl = configuration.GetSection(nameof(AutoEmbedUrl)).Get<string>() ?? throw new InvalidOperationException($"{nameof(AutoEmbedUrl)} key is not found inside 'appsettings.json'.");
            ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>() ?? throw new InvalidOperationException($"{nameof(ConnectionStrings)} key is not found inside 'appsettings.json'.");
            AdminDetails = configuration.GetSection(nameof(AdminDetails)).Get<AdminDetails>() ?? throw new InvalidOperationException($"{nameof(AdminDetails)} key is not found inside 'appsettings.json'.");
            SmtpConfig = configuration.GetSection(nameof(SmtpConfig)).Get<SmtpConfig>() ?? throw new InvalidOperationException($"{nameof(SmtpConfig)} key is not found inside 'appsettings.json'.");
        }

    }
    public class AdminDetails
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ProfilePicturePath { get; set; }
    }

    public class ConnectionStrings
    {
        public required string DefaultConnection { get; set; }
    }

    public class Logging
    {
        public required LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public required string Default { get; set; }

        [JsonPropertyName("Microsoft.AspNetCore")]
        public required string MicrosoftAspNetCore { get; set; }
    }
    public class SmtpConfig
    {
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
