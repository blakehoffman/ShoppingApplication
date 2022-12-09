CREATE TABLE [dbo].[Products]
(
	Id              uniqueidentifier    NOT NULL,
	ClusterId       int                 NOT NULL IDENTITY UNIQUE,
	[Name]          varchar(100)        NOT NULL,
	[Description]   varchar(300)        NOT NULL,
	CategoryId      uniqueidentifier    NOT NULL,
	Price           decimal(10, 2)      NOT NULL,
	SysStartTime    datetime2           GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime      datetime2           GENERATED ALWAYS AS ROW END   NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkProducts PRIMARY KEY NONCLUSTERED (Id),
	CONSTRAINT fkProductsCategoryId FOREIGN KEY (CategoryId) REFERENCES Categories (Id),
	INDEX ixProductsClusterId CLUSTERED (ClusterId)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.ProductsHistory));
GO
