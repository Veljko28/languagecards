using LanguageCards.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCards.Helpers
{
	public static class CheckAuth
	{
		public static bool Authenticate(KeyValuePair<string, string> cookie)
		{
			if (cookie.Value != null && cookie.Value == AdminModel.Password)
			{
				return true;
			}
			else return false;
		}
	}
}
