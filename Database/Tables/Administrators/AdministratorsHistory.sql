CREATE TABLE [dbo].[AdministratorsHistory]
(
    Id              int             NOT NULL,
    Email           varchar(100)    NOT NULL,
    SysStartTime    datetime2		NOT NULL,
    SysEndTime      datetime2		NOT NULL,
)
