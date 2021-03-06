USE [GoogleDB]
GO
/****** Object:  Table [dbo].[Command]    Script Date: 2018/3/26 11:04:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Command](
	[Sequence] [bigint] IDENTITY(1,1) NOT NULL,
	[CommandId] [nvarchar](36) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[AggregateRootId] [nvarchar](36) NULL,
	[MessagePayload] [nvarchar](max) NULL,
	[MessageTypeName] [nvarchar](256) NULL,
 CONSTRAINT [PK_Command] PRIMARY KEY CLUSTERED 
(
	[Sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2018/3/26 11:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentId] [nvarchar](50) NOT NULL,
	[SortIndex] [int] NOT NULL,
	[PeopleAmount] [int] NOT NULL,
	[ChildAmount] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2018/3/26 11:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [nvarchar](50) NOT NULL,
	[DepartmentId] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[RealName] [nvarchar](50) NOT NULL,
	[Sex] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventStream]    Script Date: 2018/3/26 11:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventStream](
	[Sequence] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateRootTypeName] [nvarchar](256) NOT NULL,
	[AggregateRootId] [nvarchar](36) NOT NULL,
	[Version] [int] NOT NULL,
	[CommandId] [nvarchar](36) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Events] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EventStream] PRIMARY KEY CLUSTERED 
(
	[Sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LockKey]    Script Date: 2018/3/26 11:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LockKey](
	[Name] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_LockKey] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PublishedVersion]    Script Date: 2018/3/26 11:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublishedVersion](
	[Sequence] [bigint] IDENTITY(1,1) NOT NULL,
	[ProcessorName] [nvarchar](128) NOT NULL,
	[AggregateRootTypeName] [nvarchar](256) NOT NULL,
	[AggregateRootId] [nvarchar](36) NOT NULL,
	[Version] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_PublishedVersion] PRIMARY KEY CLUSTERED 
(
	[Sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EventStream] ON 

INSERT [dbo].[EventStream] ([Sequence], [AggregateRootTypeName], [AggregateRootId], [Version], [CommandId], [CreatedOn], [Events]) VALUES (1, N'OrganizationBC.Domains.Employees.Employee', N'5ab848cc7719c22a5419d7de', 1, N'5ab848cc7719c22a5419d7df', CAST(N'2018-03-26T09:11:40.630' AS DateTime), N'{"OrganizationBC.Domains.Employees.EmployeeCreated":"{\"UserName\":\"Admin\",\"Password\":\"1000:r0MencB20kvrY9HhBGZQ5SRoaeh+2Cus:+1QimsBjiaSvLeUOJClbrYV486ZoOUmV\",\"Sex\":1,\"RealName\":\"管理员\",\"Status\":1,\"DepartmentId\":\"\",\"AggregateRootId\":\"5ab848cc7719c22a5419d7de\",\"AggregateRootStringId\":\"5ab848cc7719c22a5419d7de\",\"AggregateRootTypeName\":\"OrganizationBC.Domains.Employees.Employee\",\"Version\":1,\"Id\":\"5ab848cc7719c2374c192ce6\",\"Timestamp\":\"2018-03-26T09:11:40.6250536+08:00\",\"Sequence\":1}"}')
SET IDENTITY_INSERT [dbo].[EventStream] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Command_CommandId]    Script Date: 2018/3/26 11:04:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Command_CommandId] ON [dbo].[Command]
(
	[CommandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Employee_UserName]    Script Date: 2018/3/26 11:04:50 ******/
CREATE NONCLUSTERED INDEX [IX_Employee_UserName] ON [dbo].[Employee]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EventStream_AggId_CommandId]    Script Date: 2018/3/26 11:04:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EventStream_AggId_CommandId] ON [dbo].[EventStream]
(
	[AggregateRootId] ASC,
	[CommandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EventStream_AggId_Version]    Script Date: 2018/3/26 11:04:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EventStream_AggId_Version] ON [dbo].[EventStream]
(
	[AggregateRootId] ASC,
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PublishedVersion_AggId_Version]    Script Date: 2018/3/26 11:04:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_PublishedVersion_AggId_Version] ON [dbo].[PublishedVersion]
(
	[ProcessorName] ASC,
	[AggregateRootId] ASC,
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
