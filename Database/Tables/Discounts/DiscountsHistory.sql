CREATE TABLE [dbo].[DiscountsHistory]
(
	Id                  uniqueidentifier        NOT NULL,
	ClusterId           int                     NOT NULL,
	[Code]              varchar(50)             NOT NULL,
	Amount              decimal(10, 2)           NOT NULL,
	Active              bit                     NOT NULL,
	SysStartTime        datetime2               NOT NULL,
	SysEndTime          datetime2               NOT NULL,
)
