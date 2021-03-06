USE [chdb]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Code] [nvarchar](7) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationMolecule]    Script Date: 2018.11.03. 18:34:49 ******/
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
/****** Object:  Table [dbo].[MoleculeStatic]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoleculeStatic](
	[Name] [nvarchar](100) NOT NULL,
	[CAS] [nvarchar](12) NOT NULL,
	[M_gpermol] [float] NOT NULL,
	[d] [float] NULL,
	[mp] [nvarchar](20) NULL,
	[bp] [nvarchar](20) NULL,
	[purity] [nvarchar](20) NULL,
 CONSTRAINT [PK_MoleculeStatic] PRIMARY KEY CLUSTERED 
(
	[CAS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObservationImg]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObservationImg](
	[ReactionID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[img] [image] NOT NULL,
 CONSTRAINT [PK_ObservationImg] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[MW] [float] NOT NULL,
	[Ratio] [float] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReactionID] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LeaderID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Goal] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](2048) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectPlan]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPlan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[img] [image] NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reaction]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReactionCode] [nvarchar](50) NOT NULL,
	[ChemistID] [int] NOT NULL,
	[ChiefchemistID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[Laboratory] [nvarchar](50) NULL,
	[StartDate] [datetime] NOT NULL,
	[ClosureDate] [datetime] NULL,
	[PreviousStepID] [int] NULL,
	[Literature] [nvarchar](50) NULL,
	[Sketch] [bit] NULL,
	[ReactionImg] [image] NOT NULL,
	[ProcedureText] [nvarchar](2500) NULL,
	[Yield] [nvarchar](50) NULL,
	[Observation] [nvarchar](2500) NULL,
 CONSTRAINT [PK_Reaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reagent]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reagent](
	[MoleculeCAS] [nvarchar](12) NOT NULL,
	[ReactionID] [int] NOT NULL,
	[Ratio] [float] NOT NULL,
 CONSTRAINT [PK_Reagent] PRIMARY KEY CLUSTERED 
(
	[MoleculeCAS] ASC,
	[ReactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solvent]    Script Date: 2018.11.03. 18:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solvent](
	[MoleculeCAS] [nvarchar](12) NOT NULL,
	[ReactionID] [int] NOT NULL,
	[v] [float] NOT NULL,
 CONSTRAINT [PK_Solvent] PRIMARY KEY CLUSTERED 
(
	[MoleculeCAS] ASC,
	[ReactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StartingMaterial]    Script Date: 2018.11.03. 18:34:49 ******/
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
ALTER TABLE [dbo].[ObservationImg]  WITH CHECK ADD  CONSTRAINT [FK_ObservationImg_Reaction] FOREIGN KEY([ReactionID])
REFERENCES [dbo].[Reaction] ([ID])
GO
ALTER TABLE [dbo].[ObservationImg] CHECK CONSTRAINT [FK_ObservationImg_Reaction]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Reaction] FOREIGN KEY([ReactionID])
REFERENCES [dbo].[Reaction] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Reaction]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Person_Leader] FOREIGN KEY([LeaderID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Person_Leader]
GO
ALTER TABLE [dbo].[ProjectPlan]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPlan_ProjectPlan_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[ProjectPlan] CHECK CONSTRAINT [FK_ProjectPlan_ProjectPlan_Project]
GO
ALTER TABLE [dbo].[Reaction]  WITH CHECK ADD  CONSTRAINT [FK_Reaction_Person_Chemist] FOREIGN KEY([ChemistID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[Reaction] CHECK CONSTRAINT [FK_Reaction_Person_Chemist]
GO
ALTER TABLE [dbo].[Reaction]  WITH CHECK ADD  CONSTRAINT [FK_Reaction_Person_Chiefchemist] FOREIGN KEY([ChiefchemistID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[Reaction] CHECK CONSTRAINT [FK_Reaction_Person_Chiefchemist]
GO
ALTER TABLE [dbo].[Reaction]  WITH CHECK ADD  CONSTRAINT [FK_Reaction_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[Reaction] CHECK CONSTRAINT [FK_Reaction_Project]
GO
ALTER TABLE [dbo].[Reaction]  WITH CHECK ADD  CONSTRAINT [FK_Reaction_Reaction_Previousstep] FOREIGN KEY([PreviousStepID])
REFERENCES [dbo].[Reaction] ([ID])
GO
ALTER TABLE [dbo].[Reaction] CHECK CONSTRAINT [FK_Reaction_Reaction_Previousstep]
GO
ALTER TABLE [dbo].[Reagent]  WITH CHECK ADD  CONSTRAINT [FK_Reagent_MoleculeStatic] FOREIGN KEY([MoleculeCAS])
REFERENCES [dbo].[MoleculeStatic] ([CAS])
GO
ALTER TABLE [dbo].[Reagent] CHECK CONSTRAINT [FK_Reagent_MoleculeStatic]
GO
ALTER TABLE [dbo].[Reagent]  WITH CHECK ADD  CONSTRAINT [FK_Reagent_Reaction] FOREIGN KEY([ReactionID])
REFERENCES [dbo].[Reaction] ([ID])
GO
ALTER TABLE [dbo].[Reagent] CHECK CONSTRAINT [FK_Reagent_Reaction]
GO
ALTER TABLE [dbo].[Solvent]  WITH CHECK ADD  CONSTRAINT [FK_Solvent_MoleculeStatic] FOREIGN KEY([MoleculeCAS])
REFERENCES [dbo].[MoleculeStatic] ([CAS])
GO
ALTER TABLE [dbo].[Solvent] CHECK CONSTRAINT [FK_Solvent_MoleculeStatic]
GO
ALTER TABLE [dbo].[Solvent]  WITH CHECK ADD  CONSTRAINT [FK_Solvent_Reaction] FOREIGN KEY([ReactionID])
REFERENCES [dbo].[Reaction] ([ID])
GO
ALTER TABLE [dbo].[Solvent] CHECK CONSTRAINT [FK_Solvent_Reaction]
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
