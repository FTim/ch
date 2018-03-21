USE [vegyeszjk1]
GO
/****** Object:  Table [dbo].[ObservationImg]    Script Date: 2018.03.21. 1:17:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObservationImg](
	[ReactionID] [int] NOT NULL,
	[ID] [int] NOT NULL,
	[img] [image] NOT NULL,
 CONSTRAINT [PK_ObservationImg] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ObservationImg]  WITH CHECK ADD  CONSTRAINT [FK_ObservationImg_Reaction] FOREIGN KEY([ReactionID])
REFERENCES [dbo].[Reaction] ([ID])
GO
ALTER TABLE [dbo].[ObservationImg] CHECK CONSTRAINT [FK_ObservationImg_Reaction]
GO
