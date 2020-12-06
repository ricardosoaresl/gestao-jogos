using GestaoJogos.CrossCutting.Notification.Config;
using GestaoJogos.CrossCutting.Notification.ObjectNotification;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace GestaoJogos.CrossCutting.Notification.Services
{
    public abstract class EmailService
    {
        private readonly EmailServerConfig ec;
        const string expression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        private readonly ILogger _logger;

        public EmailService(IOptions<EmailServerConfig> emailConfig, ILogger<EmailService> logger)
        {
            this.ec = emailConfig.Value;
            _logger = logger;
        }

        protected bool SendEmail(EmailObjectNotification email)
        {
            try
            {
                var emailMessage = new MimeMessage();
                var body = new TextPart(TextFormat.Html) { Text = email.Message };

                emailMessage.From.Add(new MailboxAddress(ec.FromName, ec.FromAddress));
                emailMessage.To.Add(new MailboxAddress("", email.EmailTo));
                emailMessage.Subject = email.Subject;

                if (email.Copias != null && email.Copias.Count() > 0)
                {
                    email.Copias.ToList().ForEach(cc =>
                    {
                        emailMessage.Cc.Add(new MailboxAddress("SESC", cc));
                    });
                }

                emailMessage.Body = body;

                using (var client = new SmtpClient())
                {
                    client.LocalDomain = ec.LocalDomain;

                    client.Connect(ec.MailServerAddress, Convert.ToInt32(ec.MailServerPort), SecureSocketOptions.Auto);
                    if (ec.IsAuthenticated)
                    {
                        client.Authenticate(new NetworkCredential(ec.UserId, ec.UserPassword));
                    }
                    client.Send(emailMessage);
                    client.Disconnect(true);

                    var msg = email.Subject;
                    _logger.LogInformation("E-mail enviado com sucesso: {msg}", msg);

                    return true;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                _logger.LogInformation("Erro ao enviar e-mail: {message}", message);
                return false;
            }
        }

        public bool ValidarEmail(string email)
        {
            var regex = new Regex(expression, RegexOptions.IgnoreCase);

            if (email == null)
            {
                return false;
            }

            if (regex.IsMatch(email))
            {
                return true;
            }

            return false;
        }
    }
}
