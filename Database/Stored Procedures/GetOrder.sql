CREATE PROCEDURE [dbo].[GetOrder]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM Orders WHERE Id = @Id
END
