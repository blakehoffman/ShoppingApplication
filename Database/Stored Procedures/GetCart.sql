CREATE PROCEDURE [dbo].[GetCart]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM Carts WHERE Id = @Id
END
