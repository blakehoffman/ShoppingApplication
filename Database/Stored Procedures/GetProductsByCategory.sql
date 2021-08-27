CREATE PROCEDURE [dbo].[GetProductsByCategory]
	@CategoryId uniqueidentifier
AS
BEGIN
	SELECT * FROM Products WHERE CategoryId = @CategoryId
END
