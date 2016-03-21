USE [MyDentDB]
GO

/****** Object:  Table [dbo].[InstrumentTreatments]    Script Date: 20/03/2016 02:42:33 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InstrumentTreatments](
	[InstrumentId] [int] NOT NULL,
	[TreatmentId] [int] NOT NULL,
 CONSTRAINT [PK_InstrumentTreatments] PRIMARY KEY CLUSTERED 
(
	[InstrumentId] ASC,
	[TreatmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InstrumentTreatments]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentTreatments_InstrumentId] FOREIGN KEY([InstrumentId])
REFERENCES [dbo].[Instruments] ([InstrumentId])
GO

ALTER TABLE [dbo].[InstrumentTreatments] CHECK CONSTRAINT [FK_InstrumentTreatments_InstrumentId]
GO

ALTER TABLE [dbo].[InstrumentTreatments]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentTreatments_TreatmentId] FOREIGN KEY([TreatmentId])
REFERENCES [dbo].[Treatments] ([TreatmentId])
GO

ALTER TABLE [dbo].[InstrumentTreatments] CHECK CONSTRAINT [FK_InstrumentTreatments_TreatmentId]
GO