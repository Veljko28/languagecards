CREATE PROCEDURE [dbo].[GetRandomQuestion]
	@LanguageType NVARCHAR(50)
AS
begin
	SELECT TOP 1 * FROM [dbo].[Questions] WHERE [Language] = @LanguageType ORDER BY NEWID();
end
