CREATE PROCEDURE [dbo].[GetCategoryByName]
	@Name varchar(100)
AS
BEGIN
	DECLARE @ParentId uniqueidentifier = NULL

	IF NOT @Name IN (SELECT [Name] FROM Categories WHERE Hid.GetLevel() = 1)
	BEGIN
		SET @ParentId = (SELECT Id
						   FROM Categories
						  WHERE Hid IN (SELECT Hid.GetAncestor(Hid.GetLevel() - 1) FROM Categories WHERE [Name] = @Name))
	END

	SELECT Id, [Name], @ParentId AS ParentId
	  FROM Categories
	 WHERE [Name] = @Name
END
