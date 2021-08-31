CREATE PROCEDURE [dbo].[FindProductByName]
	@Name varchar(100)
AS
BEGIN
	SELECT * FROM Products WHERE Name = @Name
END
