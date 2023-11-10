﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace Forum.Areas.Identity.Pages.Account
{
	public class AccountController : Controller
	{
		[Authorize]
		public async Task Login(string returnUrl = "/")
		{
			var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
				.WithRedirectUri(returnUrl)
				.Build();
			await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
		}

		[Authorize]
		public async Task Logout()
		{
			var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
				// Indicate here where Auth0 should redirect the user after a logout.
				// Note that the resulting absolute Uri must be added to the
				// **Allowed Logout URLs** settings for the app.
				.WithRedirectUri(Url.Action("Index", "Home"))
				.Build();
			await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}