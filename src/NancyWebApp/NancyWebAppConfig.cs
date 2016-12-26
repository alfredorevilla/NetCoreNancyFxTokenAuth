namespace NancyWebApp
{
    public static class NancyWebAppConfig
    {
        public static bool IdentityServerEnabled { get; set; } = true;

        public static string Url { get; set; } = "http://localhost:5000";
    }
}