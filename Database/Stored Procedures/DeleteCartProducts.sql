CREATE PROCEDURE [dbo].[DeleteCartProducts]
    @Id uniqueidentifier
AS
BEGIN
    DELETE FROM CartProducts WHERE CartId = @Id
END
