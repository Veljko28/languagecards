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
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
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
				var search = viewModel.WordList.Where(x =>  x.Item1.ToLower().Contains(viewModel.WordToFind.ToLower()) || x.Item2.ToLower().Contains(viewModel.WordToFind.ToLower()));
				return View(new RussianListViewModel(search, viewModel.WordToFind));
			}
		}

		[HttpGet("/russian/edit")]
		public IActionResult Edit()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
			{
				return View();
			}
			else return RedirectToAction("Index", "Home");
		}

		[HttpGet("russian/add")]
		public IActionResult Add()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
			{
				return View();
			}
			else return RedirectToAction("Index", "Home");
		}


        [HttpGet("russian/practise")]
		public IActionResult Practise(PractiseGameViewModel model = null)
        {
			var practise = (Request.Cookies.Where(x => x.Key == "russianpractise")).FirstOrDefault();
			if (practise.Value != null)
			{
				model.HearthsLeft = practise.Value[0] - 48;
				if (practise.Value.Length == 10)
				{
					model.CorrectAnswers = int.Parse(practise.Value.Substring(2, 2));
					model.TimePassed = practise.Value.Substring(5, 5);
				}
				else
				{
					model.CorrectAnswers = practise.Value[2] - 48;
					model.TimePassed = practise.Value.Substring(4, 5);
				}
			}
			else
			{
				Response.Cookies.Append("russianpractise", "3 0 00:00", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.Now.AddMinutes(15),
					IsEssential = true
				});

				Response.Cookies.Append("russianpractisestart", DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.Now.AddMinutes(15),
					IsEssential = true
				});
			}

			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
			{
				return View(model);
			}
			else return RedirectToAction("Index", "Home");
		}

        [HttpPost("russian/practisepost")]
        public IActionResult PractisePost(PractiseGameViewModel model)
        {
			// logic for cheching answer
			//model.AnswerType = "1";
			bool correct = true; // temp variable for testing
			var practise = (Request.Cookies.Where(x => x.Key == "russianpractise")).FirstOrDefault();

			if (practise.Value.Length == 10)
			{
				model.CorrectAnswers = int.Parse(practise.Value.Substring(2, 2));
				model.TimePassed = practise.Value.Substring(5, 5);
			}
			else
			{
				model.CorrectAnswers = practise.Value[2] - 48;
				model.TimePassed = practise.Value.Substring(4, 5);
			}

			var start = (Request.Cookies.Where(x => x.Key == "russianpractisestart")).FirstOrDefault();
			string cookieval = "";
			if (correct)
            {
				cookieval += model.HearthsLeft.ToString() + " ";
				cookieval += (model.CorrectAnswers+1).ToString() + " ";
			}
			else
            {
				cookieval += (model.HearthsLeft-1).ToString() + " ";
				cookieval += model.CorrectAnswers.ToString() + " ";
			}

			long timepassed =  DateTimeOffset.Now.ToUnixTimeSeconds() - long.Parse(start.Value);
			double min = timepassed / 60, sec = timepassed % 60;
			string mn = Math.Ceiling(min).ToString(), sm = Math.Ceiling(sec).ToString();
			cookieval += (mn.Length == 1 ? "0"+mn : mn) + ":" + (sm.Length == 1 ? "0" + sm : sm);

			Random rnd = new Random();
			model.QuestionType = rnd.Next(4);
			Response.Cookies.Append("russianpractise", cookieval, new Microsoft.AspNetCore.Http.CookieOptions
			{
				Expires = DateTime.Now.AddMinutes(15),
				IsEssential = true
			});
			return RedirectToAction("Practise", "Russian", model);
        }

    }
}
