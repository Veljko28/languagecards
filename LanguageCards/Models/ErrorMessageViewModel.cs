using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCards.Models
{
	public class ErrorMessageViewModel
	{

		public ErrorMessageViewModel(string msg)
		{
			Message = msg;
		}
		public string Message { get; set; } = "";
	}
}
