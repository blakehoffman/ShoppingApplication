CREATE TABLE [dbo].[OrderProductsHistory]
(
	ClusterId		int					NOT NULL,
	OrderId			uniqueidentifier	NOT NULL,
	ProductId		uniqueidentifier	NOT NULL,
	Quantity		int					NOT NULL,
	SysStartTime	datetime2			NOT NULL,
	SysEndTime		datetime2			NOT NULL,
)