USE [MyDentDB]
GO

/****** Object:  Table [dbo].[EventInstruments]    Script Date: 28/06/2016 07:20:57 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventInstruments](
	[EventId] [int] NOT NULL,
	[InstrumentId] [int] NOT NULL,
 CONSTRAINT [PK_EventInstruments] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[InstrumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EventInstruments]  WITH CHECK ADD  CONSTRAINT [FK_EventInstruments_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
GO

ALTER TABLE [dbo].[EventInstruments] CHECK CONSTRAINT [FK_EventInstruments_EventId]
GO

ALTER TABLE [dbo].[EventInstruments]  WITH CHECK ADD  CONSTRAINT [FK_EventInstruments_InstrumentId] FOREIGN KEY([InstrumentId])
REFERENCES [dbo].[Instruments] ([InstrumentId])
GO

ALTER TABLE [dbo].[EventInstruments] CHECK CONSTRAINT [FK_EventInstruments_InstrumentId]
GO