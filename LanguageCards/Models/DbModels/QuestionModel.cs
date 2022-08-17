namespace LanguageCards.Models.DbModels
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
        public string Language { get; set; }
        public bool QuestionType { get; set; }

    }
}
