CREATE TABLE [dbo].[DiscountsHistory]
(
	Id					uniqueidentifier		NOT NULL,
	ClusterId			int						NOT NULL,
	[Name]				varchar(100)			NOT NULL,
	Amount				int						NOT NULL,
	SysStartTime		datetime2				NOT NULL,
	SysEndTime			datetime2				NOT NULL,
)
