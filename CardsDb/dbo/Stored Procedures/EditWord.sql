CREATE PROCEDURE [dbo].[EditWord]
	@EditId int,
	@EditWord NVARCHAR(100),
	@EditTranslation NVARCHAR(100)
AS
begin
	UPDATE [dbo].[Questions] SET Word = @EditWord, Translation = @EditTranslation WHERE Id = @EditId;
end