CREATE TABLE [dbo].[CartsHistory]
(
	Id				uniqueidentifier	NOT NULL,
	ClusterId		int					NOT NULL,
	UserId			uniqueidentifier	NOT NULL,
	DateCreated		datetime2			NOT NULL,
	Purchased		bit					NOT NULL,
	SysStartTime	datetime2			NOT NULL,
	SysEndTime		datetime2			NOT NULL,
)
