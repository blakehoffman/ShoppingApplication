CREATE TABLE [dbo].[CategoriesHistory]
(
	Id              uniqueidentifier    NOT NULL,
	ClusterId       int                 NOT NULL,
	Hid             hierarchyid         NULL,
	[Name]          varchar(100)        NOT NULL,
	SysStartTime    datetime2           NOT NULL,
	SysEndTime      datetime2           NOT NULL,
)
