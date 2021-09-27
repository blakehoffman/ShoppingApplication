CREATE PROCEDURE [dbo].[GetOrdersByUserID]
    @UserID uniqueidentifier
AS
BEGIN
    SELECT * FROM Orders WHERE UserId = @UserID
END
