CREATE TABLE [dbo].[Carts]
(
	Id				uniqueidentifier	NOT NULL,
	ClusterId		int					NOT NULL IDENTITY UNIQUE,
	UserId			uniqueidentifier	NOT NULL,
	DateCreated		datetime2			NOT NULL,
	Purchased		bit					NOT NULL DEFAULT 0,
	SysStartTime	datetime2			GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime		datetime2			GENERATED ALWAYS AS ROW END NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkCarts PRIMARY KEY NONCLUSTERED (Id),
	INDEX ixCartsClusterId CLUSTERED (ClusterId)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.CartsHistory));
GO