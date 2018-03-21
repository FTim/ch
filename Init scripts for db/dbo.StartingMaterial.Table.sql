USE [vegyeszjk1]
GO
/****** Object:  Table [dbo].[StartingMaterial]    Script Date: 2018.03.21. 1:17:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StartingMaterial](
	[MoleculeCAS] [nvarchar](12) NOT NULL,
	[ReactionID] [int] NOT NULL,
	[n] [float] NOT NULL,
	[m] [float] NULL,
	[v] [float] NULL,
 CONSTRAINT [PK_StartingMaterial] PRIMARY KEY CLUSTERED 
(
	[MoleculeCAS] ASC,
	[ReactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StartingMaterial]  WITH CHECK ADD  CONSTRAINT [FK_StartingMaterial_MoleculeStatic] FOREIGN KEY([MoleculeCAS])
REFERENCES [dbo].[MoleculeStatic] ([CAS])
GO
ALTER TABLE [dbo].[StartingMaterial] CHECK CONSTRAINT [FK_StartingMaterial_MoleculeStatic]
GO
ALTER TABLE [dbo].[StartingMaterial]  WITH CHECK ADD  CONSTRAINT [FK_StartingMaterial_Reaction] FOREIGN KEY([ReactionID])
REFERENCES [dbo].[Reaction] ([ID])
GO
ALTER TABLE [dbo].[StartingMaterial] CHECK CONSTRAINT [FK_StartingMaterial_Reaction]
GO
