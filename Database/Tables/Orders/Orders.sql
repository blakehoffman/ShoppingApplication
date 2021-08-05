CREATE TABLE [dbo].[Orders]
(
	Id						uniqueidentifier	NOT NULL,
	ClusterId				int					NOT NULL IDENTITY UNIQUE,
	UserId					uniqueidentifier	NOT NULL,
	OrderDate				datetime2			NOT NULL,
	DiscountId				uniqueidentifier	NULL,
	SysStartTime			datetime2			GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime				datetime2			GENERATED ALWAYS AS ROW END   NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkOrders PRIMARY KEY NONCLUSTERED (Id),
	CONSTRAINT fkOrdersDiscountId FOREIGN KEY (DiscountId) REFERENCES Discounts (Id),
	INDEX ixOrdersClusterId CLUSTERED (ClusterId)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.OrdersHistory));
GO
