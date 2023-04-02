
namespace IApplication.Services.Photos
{
    public static class CloudinarySettings
    {
        public static string Secret()
        {
            return Environment.GetEnvironmentVariable(EnvironmentVariables.CloudinaryKey);
        }

        public static class EnvironmentVariables
        {
            public static string CloudinaryKey = "CloudinaryKey";
            public static string StripeKey = "StripeKey";
        }
    }
}
