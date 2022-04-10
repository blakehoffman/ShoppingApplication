CREATE TABLE [dbo].[Categories]
(
	Id              uniqueidentifier    NOT NULL,
	ClusterId       int                 NOT NULL IDENTITY UNIQUE,
	Hid             hierarchyid         NULL,
	[Name]          varchar(100)        NOT NULL,
	SysStartTime    datetime2           GENERATED ALWAYS AS ROW START NOT NULL,
	SysEndTime      datetime2           GENERATED ALWAYS AS ROW END NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
	CONSTRAINT pkCategories PRIMARY KEY NONCLUSTERED (Id),
	INDEX ixCategoriesClusterId CLUSTERED (ClusterId)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.CategoriesHistory));
GO
