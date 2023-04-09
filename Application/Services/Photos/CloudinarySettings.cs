
namespace IApplication.Services.Photos
{
    public class CloudinarySettings
    {

        public static string Secret()
        {
            var test = Environment.GetEnvironmentVariable("CloudinaryKey");
            return Environment.GetEnvironmentVariable(EnvironmentVariables.CloudinaryKey);
        }
    }

    //will be moved to a separate file
    public static class EnvironmentVariables
    {
        public const string CloudinaryKey = "CloudinaryKey";
        public const string StripeKey = "StripeKey";
        public const string SendInBlueKey = "SendInBlueKey";
        public const string ZayadaApiKey = "ZayadaApiKey";
        public const string Redis = "Redis";
    }
}
