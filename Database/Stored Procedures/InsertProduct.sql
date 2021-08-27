CREATE PROCEDURE [dbo].[InsertProduct]
	@Id uniqueidentifier,
	@Name varchar(100),
	@Description varchar(300),
	@CategoryId uniqueidentifier,
	@Price int
AS
BEGIN
	INSERT INTO Products (Id, [Name], [Description], CategoryId, Price)
	  VALUES (@Id, @Name, @Description, @CategoryId, @Price)
END