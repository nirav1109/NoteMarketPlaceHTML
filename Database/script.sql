USE [master]
GO
/****** Object:  Database [NM]    Script Date: 08-03-2021 13:13:59 ******/
CREATE DATABASE [NM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NM.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NM] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NM] SET ARITHABORT OFF 
GO
ALTER DATABASE [NM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NM] SET RECOVERY FULL 
GO
ALTER DATABASE [NM] SET  MULTI_USER 
GO
ALTER DATABASE [NM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NM] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NM', N'ON'
GO
ALTER DATABASE [NM] SET QUERY_STORE = OFF
GO
USE [NM]
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 08-03-2021 13:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[ContactUsID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[EmailID] [varchar](50) NOT NULL,
	[Subject] [varchar](200) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[ContactUsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[CountryCode] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DownloadNotes]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadNotes](
	[DownloadNoteID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[SellerID] [int] NOT NULL,
	[BuyerID] [int] NOT NULL,
	[IsSellerHasAllowedDownload] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsAttachmentDownloaded] [bit] NOT NULL,
	[AttachmentDownloadDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[PurchasePrice] [decimal](10, 2) NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategory] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_DownloadNotes] PRIMARY KEY CLUSTERED 
(
	[DownloadNoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteCategories]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCategories](
	[NoteCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteCategories] PRIMARY KEY CLUSTERED 
(
	[NoteCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteDetails]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteDetails](
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[SellerID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ActionBy] [int] NULL,
	[AdminRemarks] [varchar](max) NULL,
	[PublishedDate] [datetime] NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategoryID] [int] NOT NULL,
	[DisplayPicture] [varchar](max) NULL,
	[NoteTypeID] [int] NOT NULL,
	[NumberOfPages] [int] NULL,
	[NoteDescription] [varchar](max) NOT NULL,
	[UniversityInformation] [varchar](200) NULL,
	[CountryID] [int] NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[ProfessorName] [varchar](100) NULL,
	[SellType] [varchar](20) NOT NULL,
	[SellPrice] [decimal](10, 2) NOT NULL,
	[PreviewUpload] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteDetails] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteReviews]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReviews](
	[NoteReviewID] [int] IDENTITY(1,1) NOT NULL,
	[ReviewByID] [int] NOT NULL,
	[NoteID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Ratings] [decimal](2, 1) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteReviews] PRIMARY KEY CLUSTERED 
(
	[NoteReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteStatus]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteStatus](
	[NoteStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteStatus] PRIMARY KEY CLUSTERED 
(
	[NoteStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteType]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteType](
	[NoteTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteType] PRIMARY KEY CLUSTERED 
(
	[NoteTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNoteAttachment]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNoteAttachment](
	[NoteAttachmentID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SellerNoteAttachment] PRIMARY KEY CLUSTERED 
(
	[NoteAttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpamReports]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpamReports](
	[SpamReportID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReportByID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Remark] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SpamReports] PRIMARY KEY CLUSTERED 
(
	[SpamReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfigurations]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfigurations](
	[SystemConfigID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigKey] [varchar](100) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SystemConfigurations] PRIMARY KEY CLUSTERED 
(
	[SystemConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserProfileID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [varchar](10) NULL,
	[SecondaryEmailAddress] [varchar](100) NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](max) NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[CountryID] [int] NOT NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08-03-2021 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ContactUs] ON 

INSERT [dbo].[ContactUs] ([ContactUsID], [FullName], [EmailID], [Subject], [Comments], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Nirav Karathiya', N'karathiyanirav@gmail.com', N'Nice', N'hiii', 1, CAST(N'2021-03-04T11:49:56.570' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ContactUs] ([ContactUsID], [FullName], [EmailID], [Subject], [Comments], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Nirav Karathiya', N'karathiyanirav@gmail.com', N'how can i sell my notes ?', N'hello,
i am nirav karathiya from abad i want to sell my notes on site.
please reply asap to explain steps for that.', 1, CAST(N'2021-03-05T10:01:41.647' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ContactUs] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'India', N'+91', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Canada', N'+33', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Astralia', N'+22', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[DownloadNotes] ON 

INSERT [dbo].[DownloadNotes] ([DownloadNoteID], [NoteID], [SellerID], [BuyerID], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasePrice], [NoteTitle], [NoteCategory], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, 1, 1, 1, 1, N'~/Member/1/14/Attachment_1_05032021.pdf;~/Member/1/14/Attachment_2_05032021.pdf;', 1, NULL, 1, CAST(200.00 AS Decimal(10, 2)), N'Maths3', N'1', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[DownloadNotes] ([DownloadNoteID], [NoteID], [SellerID], [BuyerID], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadDate], [IsPaid], [PurchasePrice], [NoteTitle], [NoteCategory], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 2, 1, 1, 1, N'~/Member/1/10/DP_05032021.JPG', 1, NULL, 1, CAST(300.00 AS Decimal(10, 2)), N'C programming', N'1', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[DownloadNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteCategories] ON 

INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'BE', N'Book for BE', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Diploma', N'Book for Diploma', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'BCA', N'Book for BCA', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteDetails] ON 

INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 1, 4, NULL, NULL, NULL, N'Maths3', 1, N'~/Member/1/9/DP_05032021.JPG', 1, 22, N'maths3 book', N'DDU', 1, N'BE', N'22212', N'sda', N'Paid', CAST(22.00 AS Decimal(10, 2)), N'~/Member/1/10/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T13:43:43.830' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 1, 1, NULL, NULL, NULL, N'C programming', 1, N'~/Member/1/10/DP_05032021.JPG', 1, 22, N'c practicalbook', N'GEC', 1, N'BCOM', N'22212', N'sda', N'Paid', CAST(22.00 AS Decimal(10, 2)), N'~/Member/1/9/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T13:52:08.193' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, 1, 4, NULL, NULL, NULL, N'python', 2, N'~/Member/1/10/DP_05032021.JPG', 1, 22, N'python book', N'GP', 1, N'BSC', N'22212', N'sdsa', N'Free', CAST(22.00 AS Decimal(10, 2)), N'~/Member/1/9/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T14:10:51.733' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, 1, 4, NULL, NULL, NULL, N'Artificial intelligence', 1, N'~/Member/1/11/DP_05032021.JPG', 1, 22, N'ai book', N'MSU', 1, N'BCA', N'22212', N'sda', N'Free', CAST(22.00 AS Decimal(10, 2)), N'~/Member/1/9/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T14:15:30.590' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, 1, 4, NULL, NULL, NULL, N'data structure', 2, N'~/Member/1/12/DP_05032021.JPG', 1, 22, N'ds book', N'GEC GN', 2, N'MCA', N'22212', N'sda', N'Free', CAST(2200.00 AS Decimal(10, 2)), N'~/Member/1/9/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T14:54:27.717' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, 1, 2, NULL, NULL, NULL, N'basic electronics', 3, N'~/Member/1/13/DP_05032021.JPG', 3, 22, N'be book', N'GEC modas', 2, N'ME', N'07', N'nkp', N'Free', CAST(80.00 AS Decimal(10, 2)), N'~/Member/1/9/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T15:11:41.957' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 1, 2, NULL, NULL, NULL, N'dbms', 3, N'~/Member/1/11/DP_05032021.JPG', 3, 22, N'nice book', N'LJ', 2, N'DIPLOMA', N'07', N'nkp', N'Free', CAST(80.00 AS Decimal(10, 2)), N'~/Member/1/9/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T15:16:15.477' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, 1, 2, NULL, NULL, NULL, N'smbi', 3, N'~/Member/1/12/DP_05032021.JPG', 1, 22, N'all content is clearly', N'Nirma', 1, N'Master', N'07', N'sdsa', N'Paid', CAST(80.00 AS Decimal(10, 2)), N'jdjdj', 1, CAST(N'2021-03-05T15:16:58.317' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (9, 1, 4, NULL, NULL, NULL, N'dmbi', 1, N'~/Member/1/9/DP_05032021.JPG', 2, 22, N'dmbi book', N'Silverok', 1, N'ELECTRICAL', N'22212', N'nkp', N'Free', CAST(80.00 AS Decimal(10, 2)), N'~/Member/1/9/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T22:26:38.580' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (10, 1, 4, NULL, NULL, NULL, N'operating system', 1, N'~/Member/1/10/DP_05032021.JPG', 2, 22, N'os book', N'LD', 2, N'be', N'22212', N'nkp', N'Paid', CAST(80.00 AS Decimal(10, 2)), N'~/Member/1/10/PREVIEW_05032021.JPG', 1, CAST(N'2021-03-05T22:32:45.663' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (11, 1, 4, NULL, NULL, NULL, N'motivational guru', 1, N'~/Member/1/11/DP_05032021.JPG', 1, 22, N'motivation by sanjay raval', N'GTU', 1, N'be', N'22212', N'sda', N'Paid', CAST(22.00 AS Decimal(10, 2)), NULL, 1, CAST(N'2021-03-05T22:37:47.830' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (12, 1, 4, NULL, NULL, NULL, N'khulikitab', 1, N'~/Member/1/12/DP_05032021.JPG', 1, 22, N'story book', N'california', 1, N'be', N'22212', N'nkp', N'Paid', CAST(80.00 AS Decimal(10, 2)), NULL, 1, CAST(N'2021-03-05T22:40:11.423' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (13, 1, 4, NULL, NULL, NULL, N'c++', 2, N'~/Member/1/13/DP_05032021.JPG', 1, 22, N'c++ logics', N'PDPU', 2, N'be', N'22212', N'nkp', N'Free', CAST(2200.00 AS Decimal(10, 2)), NULL, 1, CAST(N'2021-03-05T22:56:34.663' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (14, 1, 4, NULL, NULL, NULL, N'java', 1, N'~/Member/1/14/DP_05032021.JPG', 1, 22, N'java programming', N'Oxferd', 1, N'be', N'07', N'nkp', N'Free', CAST(2200.00 AS Decimal(10, 2)), NULL, 1, CAST(N'2021-03-05T23:12:12.803' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteReviews] ON 

INSERT [dbo].[NoteReviews] ([NoteReviewID], [ReviewByID], [NoteID], [AgainstDownloadID], [Ratings], [Comments], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 1, 1, 3, CAST(4.9 AS Decimal(2, 1)), N'this is very nice book ', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteReviews] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteStatus] ON 

INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Draft', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Published', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Rejected', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'Under Review', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteType] ON 

INSERT [dbo].[NoteType] ([NoteTypeID], [TypeName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Handwritten', N'this is handwritten book', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteType] ([NoteTypeID], [TypeName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Story Books', N'this is Story Book', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteType] ([NoteTypeID], [TypeName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'University Books', N'this is university Book', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteType] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNoteAttachment] ON 

INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 1, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T13:43:44.710' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 2, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T13:52:08.407' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, 3, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T14:10:52.397' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, 4, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T14:15:35.443' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, 5, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T14:54:50.367' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, 6, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T15:11:42.227' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 7, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T15:16:15.577' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, 8, N'jdjdjd', N'hdbchb', 1, CAST(N'2021-03-05T15:16:58.410' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (9, 9, N'DP_05032021.JPG', N'~/Member/1/14/Attachment_1_05032021.pdf;~/Member/1/14/Attachment_2_05032021.pdf;', 1, CAST(N'2021-03-05T22:26:40.023' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (10, 11, N'DP_05032021.JPG', N'~/Member/1/14/Attachment_1_05032021.pdf;~/Member/1/14/Attachment_2_05032021.pdf;', 1, CAST(N'2021-03-05T22:37:49.037' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (11, 13, N'Attachment_05032021.pdf', N'~/Member/1/14/Attachment_1_05032021.pdf;~/Member/1/14/Attachment_2_05032021.pdf;', 1, CAST(N'2021-03-05T22:56:35.123' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (12, 14, N'Attachment_1_05032021.pdf;Attachment_2_05032021.pdf;', N'~/Member/1/14/Attachment_1_05032021.pdf;~/Member/1/14/Attachment_2_05032021.pdf;', 1, CAST(N'2021-03-05T23:12:13.447' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SellerNoteAttachment] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([UserProfileID], [UserID], [DateOfBirth], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [CountryID], [University], [College], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 5, NULL, NULL, NULL, N'+91', N'9876543210', NULL, N'null', NULL, N'null', N'null', N'null', 1, NULL, NULL, 1, CAST(N'2021-03-06T19:52:53.757' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[UserProfile] ([UserProfileID], [UserID], [DateOfBirth], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [CountryID], [University], [College], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 6, NULL, NULL, NULL, N'+33', N'9876543210', NULL, N'null', NULL, N'null', N'null', N'null', 1, NULL, NULL, 1, CAST(N'2021-03-06T19:56:46.073' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([UserRoleID], [Role], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Super Admin', N'All Right', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([UserRoleID], [Role], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Admin', N'Few Rights ', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([UserRoleID], [Role], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'User', N'Site User', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserRoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 3, N'Akshay ', N'prajapati', N'karathiyanirav@gmail.com', N'Nirav', 1, CAST(N'2021-03-04T17:51:56.523' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([UserID], [UserRoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 1, N'Nirav', N'Karathiya', N'karathiyanirav2000@gmail.com', N'Nirav', 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([UserID], [UserRoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 3, N'Nirali', N'kanwani', N'nk@gmail.com', N'Nirav', 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([UserID], [UserRoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 2, N'hardi', N'patel', N'hp@gmail.com', N'hdhd', 1, CAST(N'2021-03-06T19:52:52.843' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([UserID], [UserRoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 2, N'nidhi', N'vyas', N'nv@gmail.com', N'hdhd', 1, CAST(N'2021-03-06T19:56:29.573' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[DownloadNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadNotes_BuyerID] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DownloadNotes] CHECK CONSTRAINT [FK_DownloadNotes_BuyerID]
GO
ALTER TABLE [dbo].[DownloadNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadNotes_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[DownloadNotes] CHECK CONSTRAINT [FK_DownloadNotes_NoteID]
GO
ALTER TABLE [dbo].[DownloadNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadNotes_SellerID] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DownloadNotes] CHECK CONSTRAINT [FK_DownloadNotes_SellerID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_ActionBy] FOREIGN KEY([ActionBy])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_ActionBy]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_CountryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_NoteCategoryID] FOREIGN KEY([NoteCategoryID])
REFERENCES [dbo].[NoteCategories] ([NoteCategoryID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_NoteCategoryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_NoteType] FOREIGN KEY([NoteTypeID])
REFERENCES [dbo].[NoteType] ([NoteTypeID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_NoteType]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_SellerID] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_SellerID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[NoteStatus] ([NoteStatusID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_Status]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_AgainstDownloadID] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[DownloadNotes] ([DownloadNoteID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_AgainstDownloadID]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_NoteID]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_ReviewByID] FOREIGN KEY([ReviewByID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_ReviewByID]
GO
ALTER TABLE [dbo].[SellerNoteAttachment]  WITH CHECK ADD  CONSTRAINT [FK_SellerNoteAttachment_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[SellerNoteAttachment] CHECK CONSTRAINT [FK_SellerNoteAttachment_NoteID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_AgainstDownloadID] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[DownloadNotes] ([DownloadNoteID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_AgainstDownloadID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_NoteID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_ReportByID] FOREIGN KEY([ReportByID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_ReportByID]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_CountryID]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_UserID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRoleID] FOREIGN KEY([UserRoleID])
REFERENCES [dbo].[UserRoles] ([UserRoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserRoleID]
GO
USE [master]
GO
ALTER DATABASE [NM] SET  READ_WRITE 
GO
