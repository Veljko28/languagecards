CREATE PROCEDURE [dbo].[AddImageQuestion]
	@ImagePath NVARCHAR(100),
	@Translation NVARCHAR(100),
	@Language NVARCHAR(50)
AS
begin
	INSERT INTO ImageQuestions VALUES (N''+@ImagePath, N''+@Translation, @Language);
end
