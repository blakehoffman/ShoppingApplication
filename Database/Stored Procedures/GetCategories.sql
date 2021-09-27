CREATE PROCEDURE [dbo].[GetCategories]
	@ParentId uniqueidentifier
AS
BEGIN
	DECLARE @ParentHid hierarchyid = NULL

	IF @ParentId IS NULL
	BEGIN
		SELECT Id, Name, NULL AS ParentId
		  FROM Categories
		 WHERE Hid IN (SELECT Hid FROM Categories WHERE Hid.GetLevel() = 1)
	END
	ELSE
	BEGIN
		SET @ParentHid = (SELECT Hid FROM Categories WHERE Id = @ParentId)

		SELECT Id, Name, @ParentId AS ParentId
		  FROM  Categories
		 WHERE Hid.GetAncestor(1) = @ParentHid
	END
END
