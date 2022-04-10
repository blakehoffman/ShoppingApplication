CREATE PROCEDURE [dbo].[GetCategory]
    @Id uniqueidentifier
AS
BEGIN
    DECLARE @ParentId uniqueidentifier

    IF NOT @Id IN (SELECT Id FROM Categories WHERE Hid.GetLevel() = 1)
    BEGIN
        SET @ParentId = (SELECT Id
                           FROM Categories
                          WHERE Hid IN (SELECT Hid.GetAncestor(Hid.GetLevel() - 1) FROM Categories WHERE Id = @Id))
    END

    SELECT Id, Name, @ParentId AS ParentId
      FROM Categories
     WHERE Id = @Id
END
