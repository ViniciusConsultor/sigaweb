/****** Object:  Table [dbo].[fields]    Script Date: 05/04/2009 14:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fields](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[mainId] [int] NOT NULL,
	[codigo] [varchar](10) NULL,
	[grouping] [varchar](50) NULL,
 CONSTRAINT [PK_fields] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[fields]  WITH CHECK ADD  CONSTRAINT [FK_fields_table] FOREIGN KEY([mainId])
REFERENCES [dbo].[RTable] ([id])
GO
ALTER TABLE [dbo].[fields] CHECK CONSTRAINT [FK_fields_table]