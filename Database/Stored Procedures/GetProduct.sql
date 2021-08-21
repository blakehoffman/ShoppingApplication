CREATE PROCEDURE [dbo].[GetProduct]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM Products WHERE Id = @Id
END
