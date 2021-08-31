CREATE PROCEDURE [dbo].[FindDiscountByCode]
	@Code varchar(50)
AS
BEGIN
	SELECT * FROM Discounts WHERE Code = @Code
END
