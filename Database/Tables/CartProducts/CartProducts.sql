CREATE TABLE [dbo].[CartProducts]
(
	ClusterId		int					NOT NULL IDENTITY UNIQUE,
	CartId			uniqueidentifier	NULL,
	ProductId		uniqueidentifier	NOT NULL,
	Quantity		int					NOT	NULL,
	SysStartTime	datetime2			GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime		datetime2			GENERATED ALWAYS AS ROW END NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkCartProducts PRIMARY KEY NONCLUSTERED (CartId, ProductId),
	CONSTRAINT fkCartProductsCartId FOREIGN KEY (CartId) REFERENCES Carts (Id),
	CONSTRAINT fkCartProductsProductId FOREIGN KEY (ProductId) REFERENCES Products (Id),
	INDEX ixCartProductsClusterId CLUSTERED (ClusterId)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.CartProductsHistory));
GO