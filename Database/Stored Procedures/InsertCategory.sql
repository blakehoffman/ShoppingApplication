CREATE PROCEDURE [dbo].[InsertCategory]
	@Id uniqueidentifier,
	@Name varchar(100),
	@ParentId uniqueidentifier
AS
BEGIN
	DECLARE @ParentHid hierarchyid
	DECLARE @NextChildHid hierarchyid

	IF @ParentId IS NULL
	BEGIN
		SET @ParentHid = '/'
	END
	ELSE
	BEGIN
		SET @ParentHid = (SELECT Hid FROM Categories WHERE Id = @ParentId)
	END

	EXEC GetNextChildHidFromParent @TableName = 'Categories', @ParentHid = @ParentHid, @NextChild = @NextChildHid OUTPUT

	INSERT INTO Categories (Id, [Name], Hid)
	  VALUES (@Id, @Name, @NextChildHid)
END
