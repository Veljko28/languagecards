using LanguageCards.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LanguageCards.Helpers
{
	public class CustomAuthHandler : AuthenticationHandler<CustomAuthSchemeOptions>
	{
		public CustomAuthHandler(
			IOptionsMonitor<CustomAuthSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}
		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var cookie = Request.Cookies.Where(x => x.Key == "LoggedIn");
			if (cookie == null)
			{
				return Task.FromResult(AuthenticateResult.Fail("Failed"));
			}
			else if (cookie.First().Key != AdminModel.Password)
			{
				return Task.FromResult(AuthenticateResult.Fail("Failed"));
			}
			else
			{
				var claims = new[] {
					new Claim(ClaimTypes.NameIdentifier, "LoggedIn")
				};

				var claimsIdentity = new ClaimsIdentity(claims,
							nameof(CustomAuthHandler));

				var ticket = new AuthenticationTicket(
					new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

				return Task.FromResult(AuthenticateResult.Success(ticket));
			}

		}
	}
}
