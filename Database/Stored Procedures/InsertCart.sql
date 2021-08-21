CREATE PROCEDURE [dbo].[InsertCart]
	@Id uniqueidentifier,
	@UserId uniqueidentifier,
	@DateCreated datetime2,
	@Purchased bit
AS
BEGIN
	INSERT INTO Carts (Id, UserId, DateCreated, Purchased)
	  VALUES (@Id, @UserId, @DateCreated, @Purchased)
END
