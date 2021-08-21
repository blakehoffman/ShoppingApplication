CREATE PROCEDURE [dbo].[GetCartProducts]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM CartProducts WHERE CartId = @Id
END
