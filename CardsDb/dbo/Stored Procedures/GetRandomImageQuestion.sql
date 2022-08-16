CREATE PROCEDURE [dbo].[GetRandomImageQuestion]
	@LanguageType NVARCHAR(50)
AS
begin
	SELECT TOP 1 * FROM [dbo].[ImageQuestions] WHERE [Language] = @LanguageType ORDER BY NEWID();
end
