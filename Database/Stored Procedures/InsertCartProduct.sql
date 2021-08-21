CREATE PROCEDURE [dbo].[InsertCartProduct]
	@CartId uniqueidentifier,
	@ProductId uniqueidentifier,
	@Quantity int
AS
BEGIN
	INSERT INTO CartProducts (CartId, ProductId, Quantity)
	  VALUES (@CartId, @ProductId, @Quantity)
END
