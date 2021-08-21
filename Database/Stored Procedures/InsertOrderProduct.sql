CREATE PROCEDURE [dbo].[InsertOrderProduct]
	@OrderId uniqueidentifier,
	@ProductId uniqueidentifier,
	@Quantity int
AS
BEGIN
	INSERT INTO OrderProducts (OrderId, ProductId, Quantity)
	  VALUES (@OrderId, @ProductId, @Quantity)
END
