CREATE PROCEDURE [dbo].[GetCartByUserID]
    @UserId uniqueidentifier
AS
BEGIN
    SELECT * FROM Carts WHERE UserId = @UserId AND Purchased = 0
END
