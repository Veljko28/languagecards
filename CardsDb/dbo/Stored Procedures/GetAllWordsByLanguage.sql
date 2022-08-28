CREATE PROCEDURE [dbo].[GetAllWordsByLanguage]
	@LanguageType  NVARCHAR(50)
AS
begin
	SELECT * FROM Questions WHERE [Language] = @LanguageType;
end