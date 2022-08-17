using LanguageCards.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace LanguageCards.Helpers
{
    public class CardDbContext : DbContext
    {
        public CardDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=CardsDb;Trusted_Connection=True");
        }

        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<ImageQuestionModel> ImageQuestions { get; set; }
    }
}
