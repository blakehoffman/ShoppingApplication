CREATE PROCEDURE [dbo].[GetOrderProducts]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM OrderProducts WHERE OrderId = @Id
END
