using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BWERP.Api.Repositories.Services
{
	public class MailService : IMailService
	{
		private readonly IConfiguration _config;
		public MailService(IConfiguration config)
		{
			_config = config;
		}
		public async void SendEmailAsync(EmailDto emailDto)
		{
			string[] recipients = emailDto.ToAdress.Split(';');

			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse("notice-admin@buwon.com"));

			// Add recipients to the email
			InternetAddressList recipientList = new InternetAddressList();
			foreach (var recipient in recipients)
			{
				if (MailboxAddress.TryParse(recipient.Trim(), out var mailboxAddress))
				{
					recipientList.Add(mailboxAddress);
				}
				else
				{
					// Handle invalid recipient address
					Console.WriteLine($"Invalid recipient address: {recipient}");
				}
			}
			email.To.AddRange(recipientList);

			// Add CC if provided
			if (!string.IsNullOrEmpty(emailDto.CcAddress))
			{
				string[] ccAddresses = emailDto.CcAddress.Split(';');
				foreach (var ccAddress in ccAddresses)
				{
					if (MailboxAddress.TryParse(ccAddress.Trim(), out var mailboxAddress))
					{
						email.Cc.Add(mailboxAddress);
					}
					else
					{
						// Handle invalid CC address
						Console.WriteLine($"Invalid CC address: {ccAddress}");
					}
				}
			}

			email.Subject = emailDto.Subject;

			// Create the email body
			var builder = new BodyBuilder
			{
				HtmlBody = $@"<html><body><ul>{emailDto.Body}</ul></body></html>"
			};
			email.Body = builder.ToMessageBody();

			try
			{
				using var smtp = new SmtpClient();
				await smtp.ConnectAsync(_config.GetSection("SmtpServer").Value, 465, SecureSocketOptions.SslOnConnect);
				await smtp.AuthenticateAsync("notice-admin@buwon.com", _config.GetSection("Password").Value);
				await smtp.SendAsync(email);
				await smtp.DisconnectAsync(true);
			}
			catch (Exception ex)
			{
				// Log the exception
				Console.WriteLine($"Error sending email: {ex.Message}");
				// Optionally rethrow or handle the exception as needed
			}
		}
	}
}
