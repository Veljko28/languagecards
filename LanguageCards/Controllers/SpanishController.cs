using LanguageCards.Helpers;
using LanguageCards.Models;
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
				if (CheckAuth.Authenticate(cookie, AdminModel.Password))
				{
					return View();
				}
				else return RedirectToAction("Index", "Home");
			}


		[HttpGet("spanish/add")]
		public IActionResult Add()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
			{
				return View();
			}
			else return RedirectToAction("Index", "Home");
		}


		[HttpGet("/spanish/search")]
		public IActionResult Search()
		{
			RussianListViewModel viewModel = new RussianListViewModel();

			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
			{
				return View(viewModel);
			}
			else return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Search(RussianListViewModel viewModel)
		{
			if (String.IsNullOrEmpty(viewModel.WordToFind))
			{
				viewModel.WordList = RussianListViewModel.TempList;
				return View(viewModel);
			}
			else
			{
				var search = viewModel.WordList.Where(x => x.Item1.ToLower().Contains(viewModel.WordToFind.ToLower()) || x.Item2.ToLower().Contains(viewModel.WordToFind.ToLower()));
				return View(new RussianListViewModel(search, viewModel.WordToFind));
			}
		}

        [HttpGet("/spanish/practise")]
		public IActionResult Practise()
        {

			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
			{
				return View(new PractiseGameViewModel());
			}
			else return RedirectToAction("Index", "Home");
		}
	}
}
