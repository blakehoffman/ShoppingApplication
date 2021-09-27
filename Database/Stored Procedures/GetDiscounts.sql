CREATE PROCEDURE [dbo].[GetDiscounts]
AS
BEGIN
	SELECT * FROM Discounts WHERE Active = 1
END
