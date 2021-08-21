CREATE PROCEDURE [dbo].[InsertProduct]
	@Id uniqueidentifier,
	@Name varchar(100),
	@Description varchar(300),
	@CategoryId uniqueidentifier
AS
BEGIN
	INSERT INTO Products (Id, [Name], [Description], CategoryId)
	  VALUES (@Id, @Name, @Description, @CategoryId)
END