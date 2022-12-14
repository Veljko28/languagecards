namespace LanguageCards.Models
{
    public class PractiseGameViewModel
    {
        public int HearthsLeft { get; set; } = 3;
        public int CorrectAnswers { get; set; } = 0;
        public string TimePassed { get; set; } = "00:00";
        public string Answer { get; set; } = ""; // Answer given by user
        public string AnswerType { get; set; } = "0"; // if 0 no display, if 1 display that the answer is correct, if 2 incorrect
        public int QuestionType { get; set; } = 1;
        public string Word { get; set; }
        public string Translation { get; set; } // database translation / correct answer
        public string ImagePath { get; set; }
    }
}
