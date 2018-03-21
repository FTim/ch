USE [vegyeszjk1]
GO
/****** Object:  Table [dbo].[LocationMolecule]    Script Date: 2018.03.21. 1:17:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationMolecule](
	[LocationID] [nvarchar](7) NOT NULL,
	[MoleculeCAS] [nvarchar](12) NOT NULL,
	[m] [float] NULL,
	[v] [float] NULL,
 CONSTRAINT [PK_LocationMolecule] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[MoleculeCAS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocationMolecule]  WITH CHECK ADD  CONSTRAINT [FK_LocationMolecule_Location] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Location] ([Code])
GO
ALTER TABLE [dbo].[LocationMolecule] CHECK CONSTRAINT [FK_LocationMolecule_Location]
GO
ALTER TABLE [dbo].[LocationMolecule]  WITH CHECK ADD  CONSTRAINT [FK_LocationMolecule_LocationMolecule] FOREIGN KEY([MoleculeCAS])
REFERENCES [dbo].[MoleculeStatic] ([CAS])
GO
ALTER TABLE [dbo].[LocationMolecule] CHECK CONSTRAINT [FK_LocationMolecule_LocationMolecule]
GO
