CREATE TABLE [dbo].[CartProductsHistory]
(
    ClusterId		int					NOT NULL,
    CartId			uniqueidentifier	NOT NULL,
    ProductId		uniqueidentifier	NOT NULL,
    Quantity		int					NOT NULL,
    SysStartTime	datetime2			NOT NULL,
    SysEndTime		datetime2			NOT NULL
)