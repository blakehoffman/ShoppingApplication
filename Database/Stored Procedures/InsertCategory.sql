CREATE PROCEDURE [dbo].[InsertCategory]
	@Id uniqueidentifier,
	@Name varchar(100),
	@ParentId uniqueidentifier
AS
BEGIN
	DECLARE @ParentHid hierarchyid = (SELECT Hid FROM Categories WHERE Id = @ParentId)
	DECLARE @NextChildHid hierarchyid

	EXEC GetNextChildHidFromParent @TableName = 'Categories', @ParentHid = @ParentHid, @NextChild = @NextChildHid OUTPUT

	INSERT INTO Categories (Id, [Name], Hid)
	  VALUES (@Id, @Name, @NextChildHid)
END
