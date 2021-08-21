CREATE PROCEDURE [dbo].[InsertDiscount]
	@Id uniqueidentifier,
	@Code varchar(50),
	@Amount decimal(2, 2),
	@Active bit
AS
BEGIN
	INSERT INTO Discounts (Id, Code, Amount, Active)
	  VALUES (@Id, @Code, @Amount, @Active)
END
