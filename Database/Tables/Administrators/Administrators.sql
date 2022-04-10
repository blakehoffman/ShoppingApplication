CREATE TABLE [dbo].[Administrators]
(
	Id				int             NOT NULL IDENTITY(1, 1),
	Email			varchar(100)	NOT NULL UNIQUE,
	SysStartTime	datetime2		GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime		datetime2		GENERATED ALWAYS AS ROW END NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkAdministrators PRIMARY KEY CLUSTERED (Id),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.AdministratorsHistory));
GO
