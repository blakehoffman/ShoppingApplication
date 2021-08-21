CREATE PROCEDURE [dbo].[GetCategories]
	@ParentId uniqueidentifier
AS
BEGIN
	DECLARE @ParentHid hierarchyid = (SELECT Hid FROM Categories WHERE Id = @ParentId)

	SELECT *, Hid.ToString()
	  FROM  Categories
	 WHERE Hid.GetAncestor(1) = @ParentHid
END
