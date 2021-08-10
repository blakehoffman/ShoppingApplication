CREATE TABLE [dbo].[Discounts]
(
	Id						uniqueidentifier	NOT NULL,
	ClusterId				int					NOT NULL IDENTITY UNIQUE,
	[Name]					varchar(100)		NOT NULL,
	Amount					decimal(2, 2)		NOT NULL,
	Active					bit					NOT NULL,
	SysStartTime			datetime2			GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime				datetime2			GENERATED ALWAYS AS ROW END NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkDiscounts PRIMARY KEY NONCLUSTERED (Id),
	INDEX ixDiscountsClusterId CLUSTERED (ClusterId)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.DiscountsHistory));
GO
