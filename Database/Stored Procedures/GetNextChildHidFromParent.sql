CREATE PROCEDURE [dbo].[GetNextChildHidFromParent]
	@TableName varchar(100),
	@ParentHid hierarchyid,
	@NextChild hierarchyid OUTPUT
AS
BEGIN
  	DECLARE @LastNodeOut hierarchyid;
	DECLARE @Stmt nvarchar(200);
	
	SET @Stmt =	N'(SELECT @LastNode = MAX(Hid)
                     FROM ' + @TableName +
                   'WHERE Hid.GetAncestor(1) = @Parent)'
	
	DECLARE @Params nvarchar(100) = N'@Parent hierarchyid,
                                      @LastNode hierarchyid OUTPUT'

	EXECUTE sp_executesql @stmt, @Params, @Parent = @ParentHid, @LastNode = @LastNodeOut OUTPUT

	SET @NextChild = (SELECT @ParentHid.GetDescendant(@LastNodeOut, NULL).ToString())
END
