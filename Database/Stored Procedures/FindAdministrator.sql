CREATE PROCEDURE [dbo].[FindAdministrator]
    @Email varchar(100)
AS
BEGIN
    SELECT * FROM Administrators WHERE Email = @Email
END
