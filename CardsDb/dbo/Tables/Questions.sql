CREATE TABLE [dbo].[Questions]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Word] NVARCHAR(100) NOT NULL,
	[Translation] NVARCHAR(100) NOT NULL,
	[Language] NVARCHAR(50) NOT NULL,
	[QuestionType] bit NOT NULL
)
