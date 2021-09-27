CREATE PROCEDURE [dbo].[UpdateCart]
    @CartId uniqueidentifier,
    @Purchased bit
AS
BEGIN
    UPDATE Carts SET Purchased = @Purchased WHERE Id = @CartId
END
