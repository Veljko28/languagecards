namespace LanguageCards.Models.DbModels
{
    public class ImageQuestionModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Translation { get; set; }
        public string Language { get; set; }
    }
}
