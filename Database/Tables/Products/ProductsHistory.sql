CREATE TABLE [dbo].[ProductsHistory]
(
	Id              uniqueidentifier    NOT NULL,
	ClusterId       int                 NOT NULL,
	[Name]          varchar(100)        NOT NULL,
	[Description]   varchar(300)        NOT NULL,
	CategoryId      uniqueidentifier    NOT NULL,
	Price           decimal(10,2)       NOT NULL,
	SysStartTime    datetime2           NOT NULL,
	SysEndTime      datetime2           NOT NULL,
)
