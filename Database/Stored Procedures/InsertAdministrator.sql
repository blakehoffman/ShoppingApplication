CREATE PROCEDURE [dbo].[InsertAdministrator]
    @Email varchar(100)
AS
BEGIN
    INSERT INTO Administrators (Email) VALUES (@Email)
END
