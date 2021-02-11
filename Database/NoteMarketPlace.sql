USE [master]
GO
/****** Object:  Database [NoteMarketPlace]    Script Date: 11-Feb-21 5:24:13 PM ******/
CREATE DATABASE [NoteMarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NoteMarketPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NoteMarketPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NoteMarketPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NoteMarketPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NoteMarketPlace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NoteMarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NoteMarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NoteMarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NoteMarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [NoteMarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [NoteMarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NoteMarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NoteMarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NoteMarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NoteMarketPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NoteMarketPlace', N'ON'
GO
ALTER DATABASE [NoteMarketPlace] SET QUERY_STORE = OFF
GO
USE [NoteMarketPlace]
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 11-Feb-21 5:24:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrators](
	[AdministratorId] [int] NOT NULL,
	[FirstName] [varbinary](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[SecondaryEmail] [varchar](100) NULL,
	[Password] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
 CONSTRAINT [PK_Administrators] PRIMARY KEY CLUSTERED 
(
	[AdministratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BooksCategories]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BooksCategories](
	[CategoryId] [int] NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[EditedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedDate] [datetime] NULL,
 CONSTRAINT [PK_BooksCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookTypes]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookTypes](
	[TypeId] [int] NOT NULL,
	[BookType] [varchar](50) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
 CONSTRAINT [PK_BookTypes] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BuyerRequest]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuyerRequest](
	[BuyerRequestId] [int] NOT NULL,
	[DownloadTime] [datetime] NOT NULL,
	[NoteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
	[Editdate] [datetime] NULL,
 CONSTRAINT [PK_BuyerRequest] PRIMARY KEY CLUSTERED 
(
	[BuyerRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[ContactUsId] [int] NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Subject] [varchar](30) NOT NULL,
	[Comment] [varchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[ContactUsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[CountryCode] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
	[EditedDate] [datetime] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MyDownload]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyDownload](
	[DownloadId] [int] NOT NULL,
	[DownloadTime] [datetime] NOT NULL,
	[NoteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
 CONSTRAINT [PK_MyDownload] PRIMARY KEY CLUSTERED 
(
	[DownloadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Note]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[NoteId] [int] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[DisplayPicture] [varchar](200) NOT NULL,
	[NoteFile] [varchar](200) NOT NULL,
	[TypeId] [int] NOT NULL,
	[NumberOfPage] [int] NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[CountryId] [varchar](50) NOT NULL,
	[InstituteName] [varchar](100) NOT NULL,
	[CourseName] [varchar](50) NOT NULL,
	[Professor] [varchar](100) NOT NULL,
	[NotePreview] [varchar](200) NOT NULL,
	[SellPrice] [float] NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[AdministratorId] [int] NOT NULL,
	[RejectedRemark] [varchar](200) NOT NULL,
	[RejectedDate] [datetime] NOT NULL,
	[ApprovedBy] [int] NOT NULL,
	[CoureseCode] [int] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
	[EditedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportedNotes]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportedNotes](
	[ReportId] [int] NOT NULL,
	[ReportRemark] [varchar](200) NOT NULL,
	[AsInappropriate] [bit] NOT NULL,
	[NoteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
	[EditedDate] [datetime] NULL,
 CONSTRAINT [PK_ReportedNotes] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SoldBooks]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoldBooks](
	[SoldBookId] [int] NOT NULL,
	[DownloadTime] [datetime] NOT NULL,
	[NoteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
 CONSTRAINT [PK_SoldBooks] PRIMARY KEY CLUSTERED 
(
	[SoldBookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfiguration]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfiguration](
	[SystemConfigId] [int] NOT NULL,
	[DefaultUserProfile] [varchar](200) NOT NULL,
	[DefaultNoteImage] [varchar](200) NOT NULL,
	[LinkedInUrl] [varchar](100) NOT NULL,
	[TweeterUrl] [varchar](100) NOT NULL,
	[FacebookUrl] [varchar](100) NOT NULL,
	[SecondaryEmail] [varchar](100) NOT NULL,
	[Phone] [bigint] NOT NULL,
	[SupportedEmail] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
	[EditedDate] [datetime] NULL,
 CONSTRAINT [PK_SystemConfiguration] PRIMARY KEY CLUSTERED 
(
	[SystemConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Phone] [bigint] NULL,
	[AddedDate] [datetime] NULL,
	[AddedBy] [int] NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersReview]    Script Date: 11-Feb-21 5:24:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersReview](
	[ReviewId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[NoteId] [int] NOT NULL,
	[ReviewComment] [varchar](200) NOT NULL,
 CONSTRAINT [PK_UsersReview] PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BuyerRequest]  WITH CHECK ADD  CONSTRAINT [FK_BuyerRequest_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[BuyerRequest] CHECK CONSTRAINT [FK_BuyerRequest_Note]
GO
ALTER TABLE [dbo].[BuyerRequest]  WITH CHECK ADD  CONSTRAINT [FK_BuyerRequest_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BuyerRequest] CHECK CONSTRAINT [FK_BuyerRequest_Users]
GO
ALTER TABLE [dbo].[MyDownload]  WITH CHECK ADD  CONSTRAINT [FK_MyDownload_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[MyDownload] CHECK CONSTRAINT [FK_MyDownload_Note]
GO
ALTER TABLE [dbo].[MyDownload]  WITH CHECK ADD  CONSTRAINT [FK_MyDownload_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[MyDownload] CHECK CONSTRAINT [FK_MyDownload_Users]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_Administrators] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[Administrators] ([AdministratorId])
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_Administrators]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_BooksCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[BooksCategories] ([CategoryId])
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_BooksCategories]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_BookTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[BookTypes] ([TypeId])
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_BookTypes]
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Note] CHECK CONSTRAINT [FK_Note_Users]
GO
ALTER TABLE [dbo].[ReportedNotes]  WITH CHECK ADD  CONSTRAINT [FK_ReportedNotes_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[ReportedNotes] CHECK CONSTRAINT [FK_ReportedNotes_Note]
GO
ALTER TABLE [dbo].[ReportedNotes]  WITH CHECK ADD  CONSTRAINT [FK_ReportedNotes_Users1] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ReportedNotes] CHECK CONSTRAINT [FK_ReportedNotes_Users1]
GO
ALTER TABLE [dbo].[SoldBooks]  WITH CHECK ADD  CONSTRAINT [FK_SoldBooks_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[SoldBooks] CHECK CONSTRAINT [FK_SoldBooks_Note]
GO
ALTER TABLE [dbo].[SoldBooks]  WITH CHECK ADD  CONSTRAINT [FK_SoldBooks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SoldBooks] CHECK CONSTRAINT [FK_SoldBooks_Users]
GO
ALTER TABLE [dbo].[UsersReview]  WITH CHECK ADD  CONSTRAINT [FK_UsersReview_Note] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([NoteId])
GO
ALTER TABLE [dbo].[UsersReview] CHECK CONSTRAINT [FK_UsersReview_Note]
GO
ALTER TABLE [dbo].[UsersReview]  WITH CHECK ADD  CONSTRAINT [FK_UsersReview_Users1] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UsersReview] CHECK CONSTRAINT [FK_UsersReview_Users1]
GO
USE [master]
GO
ALTER DATABASE [NoteMarketPlace] SET  READ_WRITE 
GO
