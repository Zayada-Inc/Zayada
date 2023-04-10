using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using sib_api_v3_sdk.Api;
using IApplication.Services.Photos;
using Domain.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp-relay.sendinblue.com";
        private readonly int _port = 465;
        private readonly string _fromEmail = "zayada.inc@outlook.com";
        private readonly string? apiKey = Environment.GetEnvironmentVariable(EnvironmentVariables.SendInBlueKey);

        public EmailService() {
            if(!Configuration.Default.ApiKey.ContainsKey("api-key"))
            Configuration.Default.ApiKey.Add("api-key", apiKey);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                var apiInstance = new TransactionalEmailsApi();
                var sendSmtpEmail = new SendSmtpEmail(
                    sender: new SendSmtpEmailSender(
                        email: _fromEmail
                    ),
                    to: new List<SendSmtpEmailTo>
                    {
            new SendSmtpEmailTo(
                email: toEmail
            )
                    },
                    subject: subject,
                    htmlContent: EmailMessage(message)
                );

                await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
        }
        public string EmailMessage(string text)
        {
            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {{
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            background-color: #f4f4f4;
            padding: 30px;
            margin: 0;
            min-height: 100vh;
        }}
        .container {{
            background-color: #ffffff;
            border-radius: 10px;
            max-width: 600px;
            margin: 0 auto;
            padding: 40px;
            text-align: center;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }}
        h1 {{
            color: #333333;
            font-size: 32px;
            margin-bottom: 20px;
            font-weight: bold;
            text-transform: uppercase;
            letter-spacing: 1px;
            border-bottom: 2px solid #333;
            display: inline-block;
            padding-bottom: 5px;
        }}
        p {{
            color: #666666;
            font-size: 18px;
            line-height: 1.6;
            text-align: left;
        }}
        img {{
            max-width: 100%;
            height: auto;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Hello From Zayada!</h1>
        <p>{text}</p>
        <img src=""https://res.cloudinary.com/dgwkfkpve/image/upload/v1680804744/ozsrf72ko2ia9obn40iz.png"" alt=""La Crónica"">
    </div>
</body>
</html>";
        }

    }

    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

}
