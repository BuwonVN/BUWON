
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		private readonly IMailService _mailService;
		public EmailController(IMailService mailService)
		{
			_mailService = mailService;
		}

		[HttpPost]
		public IActionResult SendMail(EmailDto emailDto)
		{
			_mailService.SendEmailAsync(emailDto);
			return Ok();
		}
	}
}
