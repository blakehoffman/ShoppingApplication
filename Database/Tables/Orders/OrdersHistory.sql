﻿CREATE TABLE [dbo].[OrdersHistory]
(
	Id                      uniqueidentifier    NOT NULL,
	ClusterId               int                 NOT NULL,
	UserId                  uniqueidentifier    NOT NULL,
	OrderDate               datetimeoffset      NOT NULL,
	DiscountId              uniqueidentifier    NULL,
	SysStartTime            datetime2           NOT NULL,
	SysEndTime              datetime2           NOT NULL,
)
