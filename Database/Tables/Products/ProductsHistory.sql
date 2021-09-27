CREATE TABLE [dbo].[ProductsHistory]
(
	Id				uniqueidentifier	NOT NULL,
	ClusterId		int					NOT NULL,
	[Name]			VARCHAR(100)		NOT NULL,
	[Description]	VARCHAR(300)		NOT NULL,
	CategoryId		uniqueidentifier	NOT NULL,
	Price			int					NOT NULL,
	SysStartTime	datetime2			NOT NULL,
	SysEndTime		datetime2			NOT NULL,
)
