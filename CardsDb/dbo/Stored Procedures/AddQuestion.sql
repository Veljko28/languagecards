CREATE PROCEDURE [dbo].[AddQuestion]
	@Word NVARCHAR(100),
	@Translation NVARCHAR(100),
	@Language NVARCHAR(50)
AS
begin
	INSERT INTO Questions VALUES (N''+@Word, N''+@Translation, @Language);
end
