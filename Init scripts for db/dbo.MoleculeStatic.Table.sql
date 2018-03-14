USE [vegyeszjk1]
GO
/****** Object:  Table [dbo].[MoleculeStatic]    Script Date: 2018.03.13. 23:12:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoleculeStatic](
	[Name] [nvarchar](100) NOT NULL,
	[CAS] [nvarchar](12) NOT NULL,
	[M_gpermol] [float] NOT NULL,
	[d] [float] NOT NULL,
	[mp] [float] NOT NULL,
	[dp] [float] NOT NULL,
	[purity] [float] NOT NULL,
 CONSTRAINT [PK_MoleculeStatic] PRIMARY KEY CLUSTERED 
(
	[CAS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
