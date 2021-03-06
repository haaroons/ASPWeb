/****** Object:  Table [dbo].[Innovation_Management_User]    Script Date: 10/9/2014 6:11:12 PM ******/
USE [Leave_ManagementDB]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Innovation_Management_User](
	[pk_user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_firstname] [nvarchar](255) NULL,
	[user_lastname] [nvarchar](255) NULL,
	[user_name] [nvarchar](50) NULL,
	[user_email] [nvarchar](50) NULL,
	[user_password] [nvarchar](50) NULL,
	[user_area] [nvarchar](255) NULL,
	[fk_assigned_country] [int] NULL,
	[user_picture] [nvarchar](max) NULL,
	[user_isadmin] [bit] NULL,
	[user_isdeleted] [bit] NULL,
	[special_permission_1] [bit] NOT NULL,
 CONSTRAINT [PK_Innovation_Management_User] PRIMARY KEY CLUSTERED 
(
	[pk_user_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[Leave_Management_Approval_Matrix]    Script Date: 10/9/2014 6:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leave_Management_Approval_Matrix](
	[pk_approval_id] [int] IDENTITY(1,1) NOT NULL,
	[fk_requestor_id] [int] NULL,
	[fk_approver_id] [int] NULL,
 CONSTRAINT [PK_Leave_Management_Approval_Matrix] PRIMARY KEY CLUSTERED 
(
	[pk_approval_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[Leave_Management_Leave_Allowance]    Script Date: 10/9/2014 6:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leave_Management_Leave_Allowance](
	[pk_leave_allowance_id] [int] IDENTITY(1,1) NOT NULL,
	[fk_user_id] [int] NULL,
	[fk_leave_type_id] [int] NULL,
	[allowance_year] [nvarchar](50) NULL,
	[allowance_entitlement] [int] NULL,
	[allowance_taken] [float] NULL,
 CONSTRAINT [PK_Leave_Management_Leave_Allowance] PRIMARY KEY CLUSTERED 
(
	[pk_leave_allowance_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[Leave_Management_Leave_Taken]    Script Date: 10/9/2014 6:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leave_Management_Leave_Taken](
	[pk_leave_taken_id] [int] IDENTITY(1,1) NOT NULL,
	[fk_user_id] [int] NULL,
	[fk_leave_type] [int] NULL,
	[leave_days] [date] NULL,
	[is_halfday] [bit] NULL,
	[is_afternoon] [bit] NULL,
	[leave_status] [int] NULL,
	[fk_approved_user_id] [int] NULL,
 CONSTRAINT [PK_Leave_Management_Leave_Taken] PRIMARY KEY CLUSTERED 
(
	[pk_leave_taken_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[Leave_Management_Leave_Type]    Script Date: 10/9/2014 6:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leave_Management_Leave_Type](
	[pk_leave_type_id] [int] IDENTITY(1,1) NOT NULL,
	[leave_type] [nvarchar](max) NULL,
 CONSTRAINT [PK_Leave_Management_Leave_Type] PRIMARY KEY CLUSTERED 
(
	[pk_leave_type_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
SET IDENTITY_INSERT [dbo].[Innovation_Management_User] ON 

INSERT [dbo].[Innovation_Management_User] ([pk_user_id], [user_firstname], [user_lastname], [user_name], [user_email], [user_password], [user_area], [fk_assigned_country], [user_picture], [user_isadmin], [user_isdeleted], [special_permission_1]) VALUES (1, N'Admin', NULL, N'Admin', N'a@a.com', N'123', NULL, NULL, NULL, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Innovation_Management_User] OFF
SET IDENTITY_INSERT [dbo].[Leave_Management_Leave_Type] ON 

INSERT [dbo].[Leave_Management_Leave_Type] ([pk_leave_type_id], [leave_type]) VALUES (1, N'Casual')
INSERT [dbo].[Leave_Management_Leave_Type] ([pk_leave_type_id], [leave_type]) VALUES (2, N'Medical')
SET IDENTITY_INSERT [dbo].[Leave_Management_Leave_Type] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_Email]    Script Date: 10/9/2014 6:11:12 PM ******/
ALTER TABLE [dbo].[Innovation_Management_User] ADD  CONSTRAINT [Unique_Email] UNIQUE NONCLUSTERED 
(
	[user_email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF)
GO
ALTER TABLE [dbo].[Innovation_Management_User] ADD  DEFAULT ((154)) FOR [fk_assigned_country]
GO
ALTER TABLE [dbo].[Innovation_Management_User] ADD  DEFAULT ((0)) FOR [special_permission_1]
GO
ALTER TABLE [dbo].[Leave_Management_Approval_Matrix]  WITH CHECK ADD  CONSTRAINT [FK_Leave_Management_Approval_Matrix_Innovation_Management_User2] FOREIGN KEY([fk_requestor_id])
REFERENCES [dbo].[Innovation_Management_User] ([pk_user_id])
GO
ALTER TABLE [dbo].[Leave_Management_Approval_Matrix] CHECK CONSTRAINT [FK_Leave_Management_Approval_Matrix_Innovation_Management_User2]
GO
ALTER TABLE [dbo].[Leave_Management_Approval_Matrix]  WITH CHECK ADD  CONSTRAINT [FK_Leave_Management_Approval_Matrix_Innovation_Management_User3] FOREIGN KEY([fk_approver_id])
REFERENCES [dbo].[Innovation_Management_User] ([pk_user_id])
GO
ALTER TABLE [dbo].[Leave_Management_Approval_Matrix] CHECK CONSTRAINT [FK_Leave_Management_Approval_Matrix_Innovation_Management_User3]
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Allowance]  WITH CHECK ADD  CONSTRAINT [FK_Leave_Management_Leave_Allowance_Innovation_Management_User] FOREIGN KEY([fk_user_id])
REFERENCES [dbo].[Innovation_Management_User] ([pk_user_id])
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Allowance] CHECK CONSTRAINT [FK_Leave_Management_Leave_Allowance_Innovation_Management_User]
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Allowance]  WITH CHECK ADD  CONSTRAINT [FK_Leave_Management_Leave_Allowance_Leave_Management_Leave_Type] FOREIGN KEY([fk_leave_type_id])
REFERENCES [dbo].[Leave_Management_Leave_Type] ([pk_leave_type_id])
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Allowance] CHECK CONSTRAINT [FK_Leave_Management_Leave_Allowance_Leave_Management_Leave_Type]
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Taken]  WITH CHECK ADD  CONSTRAINT [FK_Leave_Management_Leave_Taken_Innovation_Management_User] FOREIGN KEY([fk_user_id])
REFERENCES [dbo].[Innovation_Management_User] ([pk_user_id])
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Taken] CHECK CONSTRAINT [FK_Leave_Management_Leave_Taken_Innovation_Management_User]
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Taken]  WITH CHECK ADD  CONSTRAINT [FK_Leave_Management_Leave_Taken_Innovation_Management_User1] FOREIGN KEY([fk_approved_user_id])
REFERENCES [dbo].[Innovation_Management_User] ([pk_user_id])
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Taken] CHECK CONSTRAINT [FK_Leave_Management_Leave_Taken_Innovation_Management_User1]
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Taken]  WITH CHECK ADD  CONSTRAINT [FK_Leave_Management_Leave_Taken_Leave_Management_Leave_Type] FOREIGN KEY([fk_leave_type])
REFERENCES [dbo].[Leave_Management_Leave_Type] ([pk_leave_type_id])
GO
ALTER TABLE [dbo].[Leave_Management_Leave_Taken] CHECK CONSTRAINT [FK_Leave_Management_Leave_Taken_Leave_Management_Leave_Type]
GO
