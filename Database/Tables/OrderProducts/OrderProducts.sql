CREATE TABLE [dbo].[OrderProducts]
(
	ClusterId       int                 NOT NULL IDENTITY UNIQUE,
	OrderId         uniqueidentifier    NOT NULL,
	ProductId       uniqueidentifier    NOT NULL,
	Quantity        int                 NOT NULL,
	SysStartTime    datetime2           GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime      datetime2           GENERATED ALWAYS AS ROW END NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkOrderProducts PRIMARY KEY NONCLUSTERED (OrderId, ProductId),
	CONSTRAINT fkOrderProductsOrderId FOREIGN KEY (OrderId) REFERENCES Orders (Id),
	CONSTRAINT fkOrderProductsProductId FOREIGN KEY (ProductId) REFERENCES Products (Id),
	INDEX ixOrderProductsClusterId CLUSTERED (ClusterId)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.OrderProductsHistory));
GO
