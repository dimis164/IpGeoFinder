USE [GeoIpDB]
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 31/1/2021 11:41:28 μμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch](
	[Id] [uniqueidentifier] NOT NULL,
	[InsertionDateTime] [datetime2](7) NULL,
	[StatusId] [smallint] NULL,
 CONSTRAINT [PK_Batch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchDetails]    Script Date: 31/1/2021 11:41:29 μμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BatchId] [uniqueidentifier] NOT NULL,
	[Ip] [nvarchar](15) NOT NULL,
	[CountryCode] [nvarchar](10) NULL,
	[CountryName] [nvarchar](70) NULL,
	[TimeZone] [nvarchar](70) NULL,
	[Latitude] [decimal](18, 9) NULL,
	[Longitude] [decimal](18, 9) NULL,
	[FetchedDateTime] [datetime2](7) NULL,
 CONSTRAINT [PK_BatchDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 31/1/2021 11:41:29 μμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [smallint] NOT NULL,
	[Literal] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Status] ([Id], [Literal]) VALUES (1, N'Running')
GO
INSERT [dbo].[Status] ([Id], [Literal]) VALUES (2, N'Completed')
GO
INSERT [dbo].[Status] ([Id], [Literal]) VALUES (3, N'Error')
GO
/****** Object:  Index [NonClusteredIndex-20210130-000253]    Script Date: 31/1/2021 11:41:29 μμ ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20210130-000253] ON [dbo].[BatchDetails]
(
	[Id] ASC,
	[BatchId] ASC,
	[FetchedDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Batch] ADD  CONSTRAINT [DF_Batch_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [FK_Batch_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [FK_Batch_Status]
GO
ALTER TABLE [dbo].[BatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_BatchDetails_Batch] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[BatchDetails] CHECK CONSTRAINT [FK_BatchDetails_Batch]
GO
