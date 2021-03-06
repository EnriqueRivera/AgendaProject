USE [master]
GO
/****** Object:  Database [MyDentDB]    Script Date: 16/01/2016 12:22:53 p.m. ******/
CREATE DATABASE [MyDentDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyDentDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MyDentDB.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MyDentDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MyDentDB_log.ldf' , SIZE = 7616KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MyDentDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyDentDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyDentDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyDentDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyDentDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyDentDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyDentDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyDentDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyDentDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MyDentDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyDentDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyDentDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyDentDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyDentDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyDentDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyDentDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyDentDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyDentDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyDentDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyDentDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyDentDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyDentDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyDentDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyDentDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyDentDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyDentDB] SET RECOVERY FULL 
GO
ALTER DATABASE [MyDentDB] SET  MULTI_USER 
GO
ALTER DATABASE [MyDentDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyDentDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyDentDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyDentDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyDentDB', N'ON'
GO
USE [MyDentDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetInventoryAvailability]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetInventoryAvailability]
	@drawerId AS INT,
	@year AS INT,
	@month AS INT
AS
BEGIN

SELECT 
	i.InstrumentId
	,i.Name AS InstrumentName
	,i.Quantity
	--,dt.year_month
	,i.TreatmentId
	,CASE WHEN i.TreatmentId IS NULL THEN '' ELSE t.Name END AS TreatmentName
	,CASE WHEN i.TreatmentId IS NULL THEN '' ELSE i.UsesLeft END AS UsesLeft
	,i.MaxUses
	,ic.InstrumentCommentId
	--,ic.CommentDate
	,CASE WHEN ic.InstrumentCommentId IS NULL THEN '' ELSE ic.Comment END AS Comment
	,CAST(CASE WHEN ic.InstrumentCommentId IS NULL THEN '0' ELSE '1' END AS bit) AS IsChecked
FROM 
	Instruments i 
	CROSS JOIN (
		SELECT CAST(CONCAT(@year, '-', @month,'-1') AS DATE) AS year_month
	) dt
	LEFT JOIN Treatments t ON i.TreatmentId = t.TreatmentId
	LEFT JOIN InstrumentComments ic ON i.InstrumentId = ic.InstrumentId
										AND year(dt.year_month) = year(ic.CommentDate) 
										AND month(dt.year_month) = month(ic.CommentDate)
WHERE 
	i.DrawerId = @drawerId
	AND i.IsDeleted = 0
ORDER BY i.InstrumentId;

END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetNextPatientAssignedId]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetNextPatientAssignedId]
AS
BEGIN
	SELECT CASE WHEN currentAssignedId IS NULL THEN 1 ELSE currentAssignedId + 1 END AS NextPatientAssignedId
	FROM (SELECT MAX(AssignedId) AS currentAssignedId FROM Patients WHERE IsDeleted = 0) dt;
END
GO
/****** Object:  Table [dbo].[AmericanExpressPaids]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AmericanExpressPaids](
	[AmericanExpressPaidId] [int] IDENTITY(1,1) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[PaidDate] [date] NOT NULL,
	[Establishment] [varchar](max) NOT NULL,
	[Concept] [varchar](max) NOT NULL,
 CONSTRAINT [PK_AmericanExpressPaids] PRIMARY KEY CLUSTERED 
(
	[AmericanExpressPaidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Authorizations]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Authorizations](
	[AuthorizationId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorizationDate] [date] NOT NULL,
	[PreAuthorizationNumber] [varchar](max) NULL,
	[AuthorizationNumber] [varchar](max) NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_Authorizations] PRIMARY KEY CLUSTERED 
(
	[AuthorizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Banks]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Banks](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BudgetDetails]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BudgetDetails](
	[BudgetDetailId] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Concept] [varchar](max) NOT NULL,
	[NumberOfEvents] [int] NOT NULL,
	[UnitCost] [decimal](18, 2) NOT NULL,
	[Discount] [int] NOT NULL,
	[UnitCostDiscount] [decimal](18, 2) NOT NULL,
	[NetTotal] [decimal](18, 2) NOT NULL,
	[TotalDiscount] [decimal](18, 2) NOT NULL,
	[TotalPerEvent] [decimal](18, 2) NOT NULL,
	[BudgetId] [int] NOT NULL,
 CONSTRAINT [PK_BudgetDetails] PRIMARY KEY CLUSTERED 
(
	[BudgetDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Budgets]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Budgets](
	[BudgetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[ExpiredDate] [date] NOT NULL,
	[Notes] [varchar](max) NOT NULL,
	[GrandTotal] [decimal](18, 2) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_Budgets] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CleanedActions]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CleanedActions](
	[CleanedActionId] [int] IDENTITY(1,1) NOT NULL,
	[Shift] [varchar](10) NOT NULL,
	[UserId] [int] NOT NULL,
	[ActionDate] [date] NOT NULL,
	[Observations] [varchar](max) NOT NULL,
 CONSTRAINT [PK_CleaningActions] PRIMARY KEY CLUSTERED 
(
	[CleanedActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CleanedMaterials]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CleanedMaterials](
	[CleanedMaterialId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[GroupLetter] [char](1) NOT NULL,
	[Cleaned] [int] NULL,
	[Packaged] [int] NULL,
	[Sterilized] [int] NULL,
	[Observations] [varchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_CleanedMaterials] PRIMARY KEY CLUSTERED 
(
	[CleanedMaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClinicHistories]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClinicHistories](
	[ClinicHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ClinicHistories] PRIMARY KEY CLUSTERED 
(
	[ClinicHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClinicHistoryAttributes]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClinicHistoryAttributes](
	[ClinicHistoryAttributeId] [int] IDENTITY(1,1) NOT NULL,
	[ClinicHistoryId] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Value] [varchar](max) NULL,
 CONSTRAINT [PK_ClinicHistoryAttributes] PRIMARY KEY CLUSTERED 
(
	[ClinicHistoryAttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Configurations]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Configurations](
	[ConfigurationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Value] [varchar](max) NULL,
 CONSTRAINT [PK_StatusEventColor] PRIMARY KEY CLUSTERED 
(
	[ConfigurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](max) NOT NULL,
	[LastName] [varchar](max) NOT NULL,
	[CellPhone] [varchar](max) NULL,
	[HomePhone] [varchar](max) NULL,
	[Address] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dotations]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dotations](
	[DotationId] [int] IDENTITY(1,1) NOT NULL,
	[DotationDate] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[UserId] [int] NULL,
	[SignedDate] [datetime] NULL,
 CONSTRAINT [PK_Dotations] PRIMARY KEY CLUSTERED 
(
	[DotationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Drawers]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Drawers](
	[DrawerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Drawers] PRIMARY KEY CLUSTERED 
(
	[DrawerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Events]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[StartEvent] [datetime] NOT NULL,
	[EndEvent] [datetime] NOT NULL,
	[IsException] [bit] NOT NULL,
	[IsCanceled] [bit] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
	[PatientSkips] [bit] NOT NULL,
	[PatientId] [int] NOT NULL,
	[TreatmentId] [int] NOT NULL,
	[EventCapturerId] [int] NOT NULL,
	[InstrumentId] [int] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventStatusChanges]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventStatusChanges](
	[EventStatusChangeId] [int] IDENTITY(1,1) NOT NULL,
	[OldStatus] [varchar](max) NULL,
	[NewStatus] [varchar](max) NOT NULL,
	[ChangeDate] [datetime] NOT NULL,
	[EventId] [int] NOT NULL,
	[StatusChangerId] [int] NOT NULL,
 CONSTRAINT [PK_EventStatusChanges] PRIMARY KEY CLUSTERED 
(
	[EventStatusChangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GeneralPaids]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GeneralPaids](
	[GeneralPaidId] [int] IDENTITY(1,1) NOT NULL,
	[ProviderName] [varchar](max) NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[TicketNumber] [varchar](max) NOT NULL,
	[PaidMethod] [varchar](max) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_GeneralPaids] PRIMARY KEY CLUSTERED 
(
	[GeneralPaidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InstrumentComments]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InstrumentComments](
	[InstrumentCommentId] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[CommentDate] [date] NOT NULL,
	[InstrumentId] [int] NOT NULL,
 CONSTRAINT [PK_InstrumentComments] PRIMARY KEY CLUSTERED 
(
	[InstrumentCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Instruments]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Instruments](
	[InstrumentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
	[DrawerId] [int] NOT NULL,
	[TreatmentId] [int] NULL,
	[UsesLeft] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[MaxUses] [int] NULL,
 CONSTRAINT [PK_Instruments] PRIMARY KEY CLUSTERED 
(
	[InstrumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventorySignatures]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventorySignatures](
	[InventorySignatureId] [int] IDENTITY(1,1) NOT NULL,
	[Signature1] [int] NOT NULL,
	[Signature2] [int] NOT NULL,
	[SignatureDate] [date] NOT NULL,
	[DrawerId] [int] NOT NULL,
 CONSTRAINT [PK_InventorySignatures] PRIMARY KEY CLUSTERED 
(
	[InventorySignatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LaboratoryWorks]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LaboratoryWorks](
	[LaboratoryWorkId] [int] IDENTITY(1,1) NOT NULL,
	[WorkName] [varchar](max) NOT NULL,
	[DeliveryDate] [date] NOT NULL,
	[ReceivedDate] [date] NULL,
	[IsDeleted] [bit] NOT NULL,
	[TechnicalId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_LaboratoryWokrs] PRIMARY KEY CLUSTERED 
(
	[LaboratoryWorkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logins]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logins](
	[LoginId] [int] IDENTITY(1,1) NOT NULL,
	[IsLogin] [bit] NOT NULL,
	[LoginDate] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Logins] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Logs]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](max) NOT NULL,
	[LogDate] [datetime] NOT NULL,
	[ErrorDetail] [varchar](max) NOT NULL,
	[MethodName] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medicines]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medicines](
	[MedicineId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Brand] [varchar](max) NOT NULL,
	[Batch] [varchar](max) NULL,
	[ExpiredDate] [date] NOT NULL,
	[Notes] [varchar](max) NULL,
	[WasReplaced] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DataCapturerId] [int] NOT NULL,
 CONSTRAINT [PK_Medicines] PRIMARY KEY CLUSTERED 
(
	[MedicineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OutgoingInvoices]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OutgoingInvoices](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [date] NULL,
	[PaidDate] [date] NOT NULL,
	[Folio] [varchar](max) NOT NULL,
	[PaidMethod] [varchar](max) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_OutgoingInvoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](max) NOT NULL,
	[LastName] [varchar](max) NOT NULL,
	[CellPhone] [varchar](max) NULL,
	[HomePhone] [varchar](max) NULL,
	[Email] [varchar](max) NULL,
	[CaptureDate] [datetime] NOT NULL,
	[HasHealthInsurance] [bit] NOT NULL,
	[ClinicHistoryId] [int] NULL,
	[DataCapturerId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDiverse] [bit] NOT NULL,
	[AssignedId] [int] NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentFolios]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentFolios](
	[FolioNumber] [int] IDENTITY(1,1) NOT NULL,
	[FolioDate] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_PaymentFolios] PRIMARY KEY CLUSTERED 
(
	[FolioNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payments]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Type] [varchar](max) NOT NULL,
	[BankId] [int] NULL,
	[VoucherCheckNumber] [varchar](max) NULL,
	[Observation] [varchar](max) NOT NULL,
	[FolioNumber] [int] NOT NULL,
	[StatementId] [int] NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PositiveBalances]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositiveBalances](
	[PositiveBalanceId] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PositiveBalanceDate] [datetime] NOT NULL,
	[AppliedDate] [datetime] NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_PositiveBalances] PRIMARY KEY CLUSTERED 
(
	[PositiveBalanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReceivedInvoices]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReceivedInvoices](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceDate] [date] NULL,
	[PurchaseDate] [date] NOT NULL,
	[Folio] [varchar](max) NOT NULL,
	[PaidMethod] [varchar](max) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[ProviderId] [int] NOT NULL,
 CONSTRAINT [PK_ReceivedInvoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reminders]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reminders](
	[ReminderId] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[AppearDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[RequireAdmin] [bit] NOT NULL,
	[Seen] [bit] NOT NULL,
	[SeenBy] [int] NULL,
	[AutoGenerated] [bit] NOT NULL,
	[SeenDate] [datetime] NULL,
 CONSTRAINT [PK_Reminders] PRIMARY KEY CLUSTERED 
(
	[ReminderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ResourceProviders]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ResourceProviders](
	[ProviderId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Providers] PRIMARY KEY CLUSTERED 
(
	[ProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Statements]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statements](
	[StatementId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
	[ReminderId] [int] NULL,
 CONSTRAINT [PK_Statements] PRIMARY KEY CLUSTERED 
(
	[StatementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Technicals]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Technicals](
	[TechnicalId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Technicals] PRIMARY KEY CLUSTERED 
(
	[TechnicalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TreatmentPayment]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentPayment](
	[TreatmentPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentPriceId] [int] NOT NULL,
	[TreatmentDate] [datetime] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Discount] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[FolioNumber] [int] NOT NULL,
	[StatementId] [int] NULL,
 CONSTRAINT [PK_TreatmentPayment] PRIMARY KEY CLUSTERED 
(
	[TreatmentPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TreatmentPrices]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TreatmentPrices](
	[TreatmentPriceId] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentKey] [varchar](max) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Discount] [int] NOT NULL,
	[Type] [varchar](max) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_TreatmentPrices] PRIMARY KEY CLUSTERED 
(
	[TreatmentPriceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Treatments]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Treatments](
	[TreatmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Duration] [int] NOT NULL,
	[Recurrent] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Treatments] PRIMARY KEY CLUSTERED 
(
	[TreatmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16/01/2016 12:22:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[AssignedUserId] [int] NOT NULL,
	[FirstName] [varchar](max) NOT NULL,
	[LastName] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Authorizations]  WITH CHECK ADD  CONSTRAINT [FK_Authorizations_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[Authorizations] CHECK CONSTRAINT [FK_Authorizations_PatientId]
GO
ALTER TABLE [dbo].[BudgetDetails]  WITH CHECK ADD  CONSTRAINT [FK_BudgetDetails_BudgetId] FOREIGN KEY([BudgetId])
REFERENCES [dbo].[Budgets] ([BudgetId])
GO
ALTER TABLE [dbo].[BudgetDetails] CHECK CONSTRAINT [FK_BudgetDetails_BudgetId]
GO
ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD  CONSTRAINT [FK_Budgets_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[Budgets] CHECK CONSTRAINT [FK_Budgets_PatientId]
GO
ALTER TABLE [dbo].[CleanedActions]  WITH CHECK ADD  CONSTRAINT [FK_CleanedActions_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[CleanedActions] CHECK CONSTRAINT [FK_CleanedActions_UserId]
GO
ALTER TABLE [dbo].[CleanedMaterials]  WITH CHECK ADD  CONSTRAINT [FK_CleanedMaterials_Cleaned] FOREIGN KEY([Cleaned])
REFERENCES [dbo].[CleanedActions] ([CleanedActionId])
GO
ALTER TABLE [dbo].[CleanedMaterials] CHECK CONSTRAINT [FK_CleanedMaterials_Cleaned]
GO
ALTER TABLE [dbo].[CleanedMaterials]  WITH CHECK ADD  CONSTRAINT [FK_CleanedMaterials_Packaged] FOREIGN KEY([Packaged])
REFERENCES [dbo].[CleanedActions] ([CleanedActionId])
GO
ALTER TABLE [dbo].[CleanedMaterials] CHECK CONSTRAINT [FK_CleanedMaterials_Packaged]
GO
ALTER TABLE [dbo].[CleanedMaterials]  WITH CHECK ADD  CONSTRAINT [FK_CleanedMaterials_Sterilized] FOREIGN KEY([Sterilized])
REFERENCES [dbo].[CleanedActions] ([CleanedActionId])
GO
ALTER TABLE [dbo].[CleanedMaterials] CHECK CONSTRAINT [FK_CleanedMaterials_Sterilized]
GO
ALTER TABLE [dbo].[ClinicHistoryAttributes]  WITH CHECK ADD  CONSTRAINT [FK_ClinicHistoryAttributes_ClinicHistoryId] FOREIGN KEY([ClinicHistoryId])
REFERENCES [dbo].[ClinicHistories] ([ClinicHistoryId])
GO
ALTER TABLE [dbo].[ClinicHistoryAttributes] CHECK CONSTRAINT [FK_ClinicHistoryAttributes_ClinicHistoryId]
GO
ALTER TABLE [dbo].[Dotations]  WITH CHECK ADD  CONSTRAINT [FK_Dotations_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Dotations] CHECK CONSTRAINT [FK_Dotations_UserId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_EventCapturerId] FOREIGN KEY([EventCapturerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_EventCapturerId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_PatientId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_TreatmentId] FOREIGN KEY([TreatmentId])
REFERENCES [dbo].[Treatments] ([TreatmentId])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_TreatmentId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [PK_Events_InstrumentId] FOREIGN KEY([InstrumentId])
REFERENCES [dbo].[Instruments] ([InstrumentId])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [PK_Events_InstrumentId]
GO
ALTER TABLE [dbo].[EventStatusChanges]  WITH CHECK ADD  CONSTRAINT [FK_EventStatusChanges_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
GO
ALTER TABLE [dbo].[EventStatusChanges] CHECK CONSTRAINT [FK_EventStatusChanges_EventId]
GO
ALTER TABLE [dbo].[EventStatusChanges]  WITH CHECK ADD  CONSTRAINT [FK_EventStatusChanges_StatusChangerId] FOREIGN KEY([StatusChangerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EventStatusChanges] CHECK CONSTRAINT [FK_EventStatusChanges_StatusChangerId]
GO
ALTER TABLE [dbo].[InstrumentComments]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentComments_InstrumentId] FOREIGN KEY([InstrumentId])
REFERENCES [dbo].[Instruments] ([InstrumentId])
GO
ALTER TABLE [dbo].[InstrumentComments] CHECK CONSTRAINT [FK_InstrumentComments_InstrumentId]
GO
ALTER TABLE [dbo].[Instruments]  WITH CHECK ADD  CONSTRAINT [FK_Instruments_DrawerId] FOREIGN KEY([DrawerId])
REFERENCES [dbo].[Drawers] ([DrawerId])
GO
ALTER TABLE [dbo].[Instruments] CHECK CONSTRAINT [FK_Instruments_DrawerId]
GO
ALTER TABLE [dbo].[Instruments]  WITH CHECK ADD  CONSTRAINT [FK_Instruments_TreatmentId] FOREIGN KEY([TreatmentId])
REFERENCES [dbo].[Treatments] ([TreatmentId])
GO
ALTER TABLE [dbo].[Instruments] CHECK CONSTRAINT [FK_Instruments_TreatmentId]
GO
ALTER TABLE [dbo].[InventorySignatures]  WITH CHECK ADD  CONSTRAINT [FK_InventorySignatures_DrawerId] FOREIGN KEY([DrawerId])
REFERENCES [dbo].[Drawers] ([DrawerId])
GO
ALTER TABLE [dbo].[InventorySignatures] CHECK CONSTRAINT [FK_InventorySignatures_DrawerId]
GO
ALTER TABLE [dbo].[InventorySignatures]  WITH CHECK ADD  CONSTRAINT [FK_InventorySignatures_Signature1] FOREIGN KEY([Signature1])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[InventorySignatures] CHECK CONSTRAINT [FK_InventorySignatures_Signature1]
GO
ALTER TABLE [dbo].[InventorySignatures]  WITH CHECK ADD  CONSTRAINT [FK_InventorySignatures_Signature2] FOREIGN KEY([Signature2])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[InventorySignatures] CHECK CONSTRAINT [FK_InventorySignatures_Signature2]
GO
ALTER TABLE [dbo].[LaboratoryWorks]  WITH CHECK ADD  CONSTRAINT [FK_LaboratoryWorks_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[LaboratoryWorks] CHECK CONSTRAINT [FK_LaboratoryWorks_PatientId]
GO
ALTER TABLE [dbo].[LaboratoryWorks]  WITH CHECK ADD  CONSTRAINT [FK_LaboratoryWorks_TechnicalId] FOREIGN KEY([TechnicalId])
REFERENCES [dbo].[Technicals] ([TechnicalId])
GO
ALTER TABLE [dbo].[LaboratoryWorks] CHECK CONSTRAINT [FK_LaboratoryWorks_TechnicalId]
GO
ALTER TABLE [dbo].[Logins]  WITH CHECK ADD  CONSTRAINT [FK_Logins_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Logins] CHECK CONSTRAINT [FK_Logins_UserId]
GO
ALTER TABLE [dbo].[Medicines]  WITH CHECK ADD  CONSTRAINT [FK_Medicines_DataCapturerId] FOREIGN KEY([DataCapturerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Medicines] CHECK CONSTRAINT [FK_Medicines_DataCapturerId]
GO
ALTER TABLE [dbo].[OutgoingInvoices]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoices_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[OutgoingInvoices] CHECK CONSTRAINT [FK_OutgoingInvoices_PatientId]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_ClinicHistoryId] FOREIGN KEY([ClinicHistoryId])
REFERENCES [dbo].[ClinicHistories] ([ClinicHistoryId])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_ClinicHistoryId]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_DataCapturerId] FOREIGN KEY([DataCapturerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_DataCapturerId]
GO
ALTER TABLE [dbo].[PaymentFolios]  WITH CHECK ADD  CONSTRAINT [FK_PaymentFolios_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PaymentFolios] CHECK CONSTRAINT [FK_PaymentFolios_PatientId]
GO
ALTER TABLE [dbo].[PaymentFolios]  WITH CHECK ADD  CONSTRAINT [FK_PaymentFolios_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[PaymentFolios] CHECK CONSTRAINT [FK_PaymentFolios_UserId]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_BankId] FOREIGN KEY([BankId])
REFERENCES [dbo].[Banks] ([BankId])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_BankId]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_FolioNumber] FOREIGN KEY([FolioNumber])
REFERENCES [dbo].[PaymentFolios] ([FolioNumber])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_FolioNumber]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_StatementId] FOREIGN KEY([StatementId])
REFERENCES [dbo].[Statements] ([StatementId])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_StatementId]
GO
ALTER TABLE [dbo].[PositiveBalances]  WITH CHECK ADD  CONSTRAINT [FK_PositiveBalances_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PositiveBalances] CHECK CONSTRAINT [FK_PositiveBalances_PatientId]
GO
ALTER TABLE [dbo].[ReceivedInvoices]  WITH CHECK ADD  CONSTRAINT [FK_ReceivedInvoices_ProviderId] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[ResourceProviders] ([ProviderId])
GO
ALTER TABLE [dbo].[ReceivedInvoices] CHECK CONSTRAINT [FK_ReceivedInvoices_ProviderId]
GO
ALTER TABLE [dbo].[Reminders]  WITH CHECK ADD  CONSTRAINT [FK_Reminders_SeenBy] FOREIGN KEY([SeenBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Reminders] CHECK CONSTRAINT [FK_Reminders_SeenBy]
GO
ALTER TABLE [dbo].[Statements]  WITH CHECK ADD  CONSTRAINT [FK_StatementId_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[Statements] CHECK CONSTRAINT [FK_StatementId_PatientId]
GO
ALTER TABLE [dbo].[Statements]  WITH CHECK ADD  CONSTRAINT [FK_Statements_ReminderId] FOREIGN KEY([ReminderId])
REFERENCES [dbo].[Reminders] ([ReminderId])
GO
ALTER TABLE [dbo].[Statements] CHECK CONSTRAINT [FK_Statements_ReminderId]
GO
ALTER TABLE [dbo].[Statements]  WITH CHECK ADD  CONSTRAINT [FK_Statements_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Statements] CHECK CONSTRAINT [FK_Statements_UserId]
GO
ALTER TABLE [dbo].[TreatmentPayment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentPayment_FolioNumber] FOREIGN KEY([FolioNumber])
REFERENCES [dbo].[PaymentFolios] ([FolioNumber])
GO
ALTER TABLE [dbo].[TreatmentPayment] CHECK CONSTRAINT [FK_TreatmentPayment_FolioNumber]
GO
ALTER TABLE [dbo].[TreatmentPayment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentPayment_StatementId] FOREIGN KEY([StatementId])
REFERENCES [dbo].[Statements] ([StatementId])
GO
ALTER TABLE [dbo].[TreatmentPayment] CHECK CONSTRAINT [FK_TreatmentPayment_StatementId]
GO
ALTER TABLE [dbo].[TreatmentPayment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentPayment_TreatmentPriceId] FOREIGN KEY([TreatmentPriceId])
REFERENCES [dbo].[TreatmentPrices] ([TreatmentPriceId])
GO
ALTER TABLE [dbo].[TreatmentPayment] CHECK CONSTRAINT [FK_TreatmentPayment_TreatmentPriceId]
GO
USE [master]
GO
ALTER DATABASE [MyDentDB] SET  READ_WRITE 
GO
