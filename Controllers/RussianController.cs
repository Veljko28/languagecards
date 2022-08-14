using LanguageCards.Helpers;
using LanguageCards.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCards.Controllers
{
	public class RussianController : Controller
	{
		[HttpGet("/russian")]
		public IActionResult Index()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie))
			{
				return View();
			}
			else return RedirectToAction("Index", "Home");
		}

		[HttpGet("/russian/search")]
		public IActionResult Search()
		{
			RussianListViewModel viewModel = new RussianListViewModel();
			
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie))
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
				var search = viewModel.WordList.Where(x =>  x.Item1.ToLower().Contains(viewModel.WordToFind.ToLower()) || x.Item2.ToLower().Contains(viewModel.WordToFind.ToLower()));
				return View(new RussianListViewModel(search, viewModel.WordToFind));
			}
		}

		[HttpGet("/russian/edit")]
		public IActionResult Edit()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie))
			{
				return View();
			}
			else return RedirectToAction("Index", "Home");
		}

		[HttpGet("russian/add")]
		public IActionResult Add()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie))
			{
				return View();
			}
			else return RedirectToAction("Index", "Home");
		}

        [HttpGet("russian/practise")]
		public IActionResult Practise()
        {
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie))
			{
				return View(new PractiseGameViewModel());
			}
			else return RedirectToAction("Index", "Home");
		}

        [HttpPost("russian/practise")]
        public IActionResult Practise(PractiseGameViewModel model)
        {
			// logic for cheching answer
			model.AnswerType = "1";
			return View(model);
        }

    }
}
