CREATE PROCEDURE [dbo].[GetDiscount]
	@Id uniqueidentifier
AS
BEGIN
  SELECT * FROM Discounts WHERE Id = @Id
END
