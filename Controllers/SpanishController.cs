using LanguageCards.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCards.Controllers
{
	public class SpanishController : Controller
	{
			[HttpGet("/spanish")]
			public IActionResult Index()
			{
				var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
				if (CheckAuth.Authenticate(cookie))
				{
					return View();
				}
				else return RedirectToAction("Index", "Home");
			}
	}
}
