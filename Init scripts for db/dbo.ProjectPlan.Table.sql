USE [vegyeszjk1]
GO
/****** Object:  Table [dbo].[ProjectPlan]    Script Date: 2018.03.21. 1:17:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPlan](
	[ID] [int] NOT NULL,
	[img] [image] NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectPlan]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPlan_ProjectPlan_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[ProjectPlan] CHECK CONSTRAINT [FK_ProjectPlan_ProjectPlan_Project]
GO
