CREATE TABLE [dbo].[DentegraAuthorizations](
	[AuthorizationId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorizationDate] [date] NOT NULL,
	[ElegibilityNumber] [varchar](max) NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_DentegraAuthorizations] PRIMARY KEY CLUSTERED 
(
	[AuthorizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DentegraAuthorizations]  WITH CHECK ADD  CONSTRAINT [FK_DentegraAuthorizations_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO

ALTER TABLE [dbo].[DentegraAuthorizations] CHECK CONSTRAINT [FK_DentegraAuthorizations_PatientId]
GO