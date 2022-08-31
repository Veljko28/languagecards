using LanguageCards.Helpers;
using LanguageCards.Models;
using LanguageCards.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCards.Controllers
{
	public class SpanishController : Controller
	{
        private readonly CardDbContext _context;

			public SpanishController(CardDbContext context)
			{
				_context = context;
			}

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

		[HttpPost("spanish/addpost")]
		public async Task<IActionResult> AddPost(AddWordModel model)
		{
			try
			{
				await _context.Questions.AddAsync(new QuestionModel(model.Foreign, model.English, "spn"));

				await _context.SaveChangesAsync();
				return RedirectToAction("Add", "Spanish");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return RedirectToAction("Error", "Home");
			}

		}


		[HttpGet("/spanish/search")]
		public IActionResult Search()
		{
			WordListViewModel viewModel = new WordListViewModel();

			List<QuestionModel> words = _context.Questions.FromSqlRaw("exec [dbo].[GetAllWordsByLanguage] @LanguageType", new SqlParameter("@LanguageType", "spn")).ToList();

			if (words.Any())
			{
				viewModel.Words = words;
			}

			var cookie = (Request.Cookies.Where(x => x.Key == "LoggedIn")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, AdminModel.Password))
			{
				return View(viewModel);
			}
			else return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Search(WordListViewModel model)
		{
			List<QuestionModel> words = _context.Questions.FromSqlRaw("exec [dbo].[GetAllWordsByLanguage] @LanguageType", new SqlParameter("@LanguageType", "spn")).ToList();

			try
			{
				var rowsModified = _context.Questions.FromSqlRaw("exec [dbo].[EditWord] @EditId, @EditWord, @EditTranslation",
					new SqlParameter("EditId", words[int.Parse(model.EditId)]),
					new SqlParameter("EditWord", model.EditWord),
					new SqlParameter("EditTranslation", model.EditTranslation));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return RedirectToAction("Search", "Spanish");
		}

        [HttpGet("/spanish/practise")]
		public IActionResult Practise(PractiseGameViewModel model)
        {

			var practise = (Request.Cookies.Where(x => x.Key == "spanishpractise")).FirstOrDefault();
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
				Response.Cookies.Append("spanishpractise", "3 0 00:00", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.Now.AddMinutes(15),
					IsEssential = true
				});

				Response.Cookies.Append("spanishpractisestart", DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), new Microsoft.AspNetCore.Http.CookieOptions
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


		[HttpPost("spanish/practisepost")]
		public IActionResult PractisePost(PractiseGameViewModel model)
		{
			// logic for cheching answer
			//model.AnswerType = "1";
			bool correct = true; // temp variable for testing
			var practise = (Request.Cookies.Where(x => x.Key == "spanishpractise")).FirstOrDefault();

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

			var start = (Request.Cookies.Where(x => x.Key == "spanishpractisestart")).FirstOrDefault();
			string cookieval = "";
			if (correct)
			{
				cookieval += model.HearthsLeft.ToString() + " ";
				cookieval += (model.CorrectAnswers + 1).ToString() + " ";
			}
			else
			{
				cookieval += (model.HearthsLeft - 1).ToString() + " ";
				cookieval += model.CorrectAnswers.ToString() + " ";
			}

			long timepassed = DateTimeOffset.Now.ToUnixTimeSeconds() - long.Parse(start.Value);
			double min = timepassed / 60, sec = timepassed % 60;
			string mn = Math.Ceiling(min).ToString(), sm = Math.Ceiling(sec).ToString();
			cookieval += (mn.Length == 1 ? "0" + mn : mn) + ":" + (sm.Length == 1 ? "0" + sm : sm);

			Random rnd = new Random();
			model.QuestionType = rnd.Next(4);
			Response.Cookies.Append("spanishpractise", cookieval, new Microsoft.AspNetCore.Http.CookieOptions
			{
				Expires = DateTime.Now.AddMinutes(15),
				IsEssential = true
			});
			if (model.CorrectAnswers == 14)
			{
				Response.Cookies.Append("esp", "true", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.Now.AddSeconds(5),
					IsEssential = true
				});
				return RedirectToAction("SuccessfulPractise", "Spanish");
			}
			else return RedirectToAction("Practise", "Spanish", model);
		}


		[HttpGet("spanish/practise/success")]
		public IActionResult SuccessfulPractise()
		{
			var cookie = (Request.Cookies.Where(x => x.Key == "esp")).FirstOrDefault();
			if (CheckAuth.Authenticate(cookie, "true"))
			{
				return View();
			}
			else return RedirectToAction("Index", "Spanish");
		}
	}
}
