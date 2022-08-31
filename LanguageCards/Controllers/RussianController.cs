using LanguageCards.Helpers;
using LanguageCards.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using LanguageCards.Models.DbModels;

namespace LanguageCards.Controllers
{
	public class RussianController : Controller
	{
		private readonly CardDbContext _context;

        public RussianController(CardDbContext context)
        {
            _context = context;
        }


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
			WordListViewModel viewModel = new WordListViewModel();

			List<QuestionModel> words = _context.Questions.FromSqlRaw("exec [dbo].[GetAllWordsByLanguage] @LanguageType", new SqlParameter("@LanguageType", "rus")).ToList();

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


        [HttpPost("/russian/searchpost")]
        public IActionResult SearchPost(WordListViewModel model)
        {
			List<QuestionModel> words = _context.Questions.FromSqlRaw("exec [dbo].[GetAllWordsByLanguage] @LanguageType", new SqlParameter("@LanguageType", "rus")).ToList();


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

			return RedirectToAction("Search", "Russian");
		}

		[HttpPost]
		public IActionResult Search(WordListViewModel viewModel)
		{
			if (String.IsNullOrEmpty(viewModel.WordToFind))
			{
				viewModel.WordList = WordListViewModel.TempList;
				return View(viewModel);
			}
			else
			{
				var search = viewModel.WordList.Where(x =>  x.Item1.ToLower().Contains(viewModel.WordToFind.ToLower()) || x.Item2.ToLower().Contains(viewModel.WordToFind.ToLower()));
				return View(new WordListViewModel(search, viewModel.WordToFind));
			}
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

        [HttpPost("russian/addpost")]
		public async Task<IActionResult> AddPost(AddWordModel model)
        {
			try
			{
				await _context.Questions.AddAsync(new QuestionModel(model.Foreign, model.English, "rus"));

				await _context.SaveChangesAsync();
				return RedirectToAction("Add", "Russian");
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
				return RedirectToAction("Error", "Home");
            }

        }


        [HttpGet("russian/practise")]
		public IActionResult Practise(PractiseGameViewModel model = null)
        {
			var practise = (Request.Cookies.Where(x => x.Key == "russianpractise")).FirstOrDefault();

			QuestionModel q = _context.Questions.FromSqlRaw("exec [dbo].[GetRandomQuestion] @LanguageType", new SqlParameter("LanguageType", "rus")).ToList().FirstOrDefault();
			model.Word = q.Word;
			model.Translation = q.Translation;


			Response.Cookies.Append("word", q.Word, new Microsoft.AspNetCore.Http.CookieOptions
			{
				Expires = DateTime.Now.AddMinutes(15),
				IsEssential = true
			});

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
        public async Task<IActionResult>PractisePost(PractiseGameViewModel model)
        {
			// logic for cheching answer
			bool correct = false;
			var answer = (Request.Cookies.Where(x => x.Key == "word")).FirstOrDefault();

			if (string.IsNullOrEmpty(answer.Value))
            {
				return RedirectToAction("Error", "Home");
            }

			QuestionModel word = await _context.Questions.FirstOrDefaultAsync(x => x.Word == answer.Value);

			if (word == null)
            {
				return RedirectToAction("Error", "Home");
			}

			if (word.Translation.ToLower() == model.Answer.ToLower())
			{
				correct = true;
			}
			else model.HearthsLeft--;


			//model.AnswerType = "1";
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
			if (model.QuestionType < 3)
            {
				QuestionModel q = _context.Questions.FromSqlRaw("exec [dbo].[GetRandomQuestion] @LanguageType", new SqlParameter("LanguageType", "rus")).ToList().FirstOrDefault();
				model.Word = q.Word;
				model.Translation = q.Translation;
			}


			Response.Cookies.Append("russianpractise", cookieval, new Microsoft.AspNetCore.Http.CookieOptions
			{
				Expires = DateTime.Now.AddMinutes(15),
				IsEssential = true
			});

			if (model.CorrectAnswers == 14)
            {
				Response.Cookies.Append("rus", "true", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.Now.AddSeconds(5),
					IsEssential = true
				});
				return RedirectToAction("SuccessfulPractise", "Russian");
            }
			else return RedirectToAction("Practise", "Russian", model);
        }


        [HttpGet("russian/practise/success")]
		public IActionResult SuccessfulPractise()
        {
            var cookie = (Request.Cookies.Where(x => x.Key == "rus")).FirstOrDefault();
            if (CheckAuth.Authenticate(cookie, "true"))
            {
                return View();
            }
            else return RedirectToAction("Index", "Russian");
        }

    }
}
