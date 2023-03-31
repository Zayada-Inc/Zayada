using System.Net.Mail;
using System.Net;

namespace ZayadaAPI.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp-mail.outlook.com";
        private readonly int _port = 587;
        private readonly string _fromEmail = "zayada.inc@outlook.com";
        private readonly string _password = "EH)s^av6R,Ji_bd";

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(_fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient(_smtpServer, _port))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_fromEmail, _password);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mail);
                }
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
        <img src=""https://upload.wikimedia.org/wikipedia/el/3/35/La_cr%C3%B3nica_.jpg"" alt=""La Crónica"">
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
