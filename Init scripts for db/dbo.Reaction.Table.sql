USE [vegyeszjk1]
GO
/****** Object:  Table [dbo].[Reaction]    Script Date: 2018.03.07. 1:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reaction](
	[ID] [int] NOT NULL,
	[ReactionCode] [nvarchar](50) NOT NULL,
	[ChemistID] [int] NOT NULL,
	[ChiefchemistID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[Laboratory] [nvarchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[ClosureDate] [datetime] NULL,
	[PreviousStepID] [int] NULL,
	[Literature] [nvarchar](50) NULL,
	[Sketch] [bit] NOT NULL,
 CONSTRAINT [PK_Reaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
