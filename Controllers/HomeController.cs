using LanguageCards.Helpers;
using LanguageCards.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace LanguageCards.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie))
			{
				return View("Dashboard");
			}
			else return View();
		}

		[HttpGet]
		public  IActionResult Dashboard()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie))
			{
				return View();
			}
			else return RedirectToAction("Index","Home");
		}


		[HttpPost]
		public IActionResult Index(UserModel user)
		{
			if (user.Username == AdminModel.Username && user.Password == AdminModel.Password)
			{
				Response.Cookies.Append("LoggedIn", AdminModel.Password, new Microsoft.AspNetCore.Http.CookieOptions { 
					Expires = DateTime.Now.AddHours(1),
					IsEssential = true
				});
				return View("Dashboard");

			}
			return View();
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
