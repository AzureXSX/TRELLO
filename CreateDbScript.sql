USE [master]
GO

/****** Object:  Database [USERS_TRELLO]    Script Date: 21.02.2023 21:37:08 ******/
CREATE DATABASE [USERS_TRELLO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'USERS_TRELLO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\USERS_TRELLO.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'USERS_TRELLO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\USERS_TRELLO_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [USERS_TRELLO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [USERS_TRELLO] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET ARITHABORT OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [USERS_TRELLO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [USERS_TRELLO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET  ENABLE_BROKER 
GO

ALTER DATABASE [USERS_TRELLO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [USERS_TRELLO] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET RECOVERY FULL 
GO

ALTER DATABASE [USERS_TRELLO] SET  MULTI_USER 
GO

ALTER DATABASE [USERS_TRELLO] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [USERS_TRELLO] SET DB_CHAINING OFF 
GO

ALTER DATABASE [USERS_TRELLO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [USERS_TRELLO] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [USERS_TRELLO] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [USERS_TRELLO] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [USERS_TRELLO] SET QUERY_STORE = ON
GO

ALTER DATABASE [USERS_TRELLO] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [USERS_TRELLO] SET  READ_WRITE 
GO

USE [USERS_TRELLO]
GO

/****** Object:  Table [dbo].[TASKS]    Script Date: 21.02.2023 21:38:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TASKS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[DateOnly] [date] NOT NULL,
	[issuedby] [nvarchar](100) NOT NULL,
	[Task_Status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TASKS]  WITH CHECK ADD CHECK  (([Description]<>''))
GO

ALTER TABLE [dbo].[TASKS]  WITH CHECK ADD CHECK  (([issuedby]<>''))
GO

ALTER TABLE [dbo].[TASKS]  WITH CHECK ADD CHECK  (([Name]<>''))
GO

ALTER TABLE [dbo].[TASKS]  WITH CHECK ADD CHECK  (([Task_Status]<>''))
GO

USE [USERS_TRELLO]
GO

/****** Object:  Table [dbo].[Stay_loged]    Script Date: 21.02.2023 21:37:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stay_loged](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[User_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stay_loged]  WITH CHECK ADD CHECK  (([User_Name]<>''))
GO

USE [USERS_TRELLO]
GO

/****** Object:  Table [dbo].[USERS]    Script Date: 21.02.2023 21:38:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[USERS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [nvarchar](100) NOT NULL,
	[User_Email] [nvarchar](100) NOT NULL,
	[User_Password] [nvarchar](100) NOT NULL,
	[User_Avatar] [varbinary](max) NULL,
	[User_Status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[User_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[User_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[USERS]  WITH CHECK ADD CHECK  (([User_Email]<>''))
GO

ALTER TABLE [dbo].[USERS]  WITH CHECK ADD CHECK  (([User_Name]<>''))
GO

ALTER TABLE [dbo].[USERS]  WITH CHECK ADD CHECK  (([User_Password]<>''))
GO

ALTER TABLE [dbo].[USERS]  WITH CHECK ADD CHECK  (([User_Status]<>''))
GO

