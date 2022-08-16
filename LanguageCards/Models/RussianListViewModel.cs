using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCards.Models
{
	public class RussianListViewModel
	{
		public RussianListViewModel(IEnumerable<Tuple<string,string>> search, string word)
		{
			WordList = search.ToList();
			WordToFind = word;
		}

		public RussianListViewModel()
		{

		}
		public static List<Tuple<string,string>> TempList = new List<Tuple<string, string>>(){
				new Tuple<string,string>("Здраствуйте","Hello"),
				new Tuple<string,string>("Возвращатся","To Go back"),
				new Tuple<string,string>("Юобка","Skirt"),
				new Tuple<string,string>("Рубашка","Shirt"),
				new Tuple<string,string>("Огурец","Cucumber"),
				new Tuple<string,string>("Марковка","Carrot"),

				new Tuple<string,string>("Здраствуйте","Hello"),
				new Tuple<string,string>("Возвращатся","To Go back"),
				new Tuple<string,string>("Юобка","Skirt"),
				new Tuple<string,string>("Рубашка","Shirt"),
				new Tuple<string,string>("Огурец","Cucumber"),
				new Tuple<string,string>("Марковка","Carrot"),

				new Tuple<string,string>("Здраствуйте","Hello"),
				new Tuple<string,string>("Возвращатся","To Go back"),
				new Tuple<string,string>("Юобка","Skirt"),
				new Tuple<string,string>("Рубашка","Shirt"),
				new Tuple<string,string>("Огурец","Cucumber"),
				new Tuple<string,string>("Марковка","Carrot"),


				new Tuple<string,string>("Здраствуйте","Hello"),
				new Tuple<string,string>("Возвращатся","To Go back"),
				new Tuple<string,string>("Юобка","Skirt"),
				new Tuple<string,string>("Рубашка","Shirt"),
				new Tuple<string,string>("Огурец","Cucumber"),
				new Tuple<string,string>("Марковка","Carrot"),
		};

		public List<Tuple<string, string>> WordList { get; set; } = TempList;

		public string WordToFind { get; set; } = "";
	}
}
