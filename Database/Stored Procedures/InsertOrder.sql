CREATE PROCEDURE [dbo].[InsertOrder]
	@Id uniqueidentifier,
	@UserId uniqueidentifier,
	@OrderDate datetime2,
	@DiscountId uniqueidentifier
AS
BEGIN
	INSERT INTO Orders (Id, UserId, OrderDate, DiscountId)
	  VALUES (@Id, @UserId, @OrderDate, @DiscountId)
END
