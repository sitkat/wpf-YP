USE [master]
GO
/****** Object:  Database [CityTelephony]    Script Date: 21.04.2022 19:50:27 ******/
CREATE DATABASE [CityTelephony]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CityTelephony', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CityTelephony.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CityTelephony_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CityTelephony_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CityTelephony] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CityTelephony].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CityTelephony] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CityTelephony] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CityTelephony] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CityTelephony] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CityTelephony] SET ARITHABORT OFF 
GO
ALTER DATABASE [CityTelephony] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CityTelephony] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CityTelephony] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CityTelephony] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CityTelephony] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CityTelephony] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CityTelephony] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CityTelephony] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CityTelephony] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CityTelephony] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CityTelephony] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CityTelephony] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CityTelephony] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CityTelephony] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CityTelephony] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CityTelephony] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CityTelephony] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CityTelephony] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CityTelephony] SET  MULTI_USER 
GO
ALTER DATABASE [CityTelephony] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CityTelephony] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CityTelephony] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CityTelephony] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CityTelephony] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CityTelephony] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CityTelephony] SET QUERY_STORE = OFF
GO
USE [CityTelephony]
GO
/****** Object:  Table [dbo].[Abonent]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Abonent](
	[ID_Abonent] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[Date_Of_Birth] [date] NOT NULL,
	[ID_Sex] [int] NOT NULL,
	[Phone_Number] [varchar](16) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Index_] [varchar](6) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Street] [varchar](50) NOT NULL,
	[House] [varchar](4) NOT NULL,
	[Flat] [varchar](4) NOT NULL,
	[ID_Station] [int] NOT NULL,
	[Benefit] [bit] NOT NULL,
	[Cross_City] [bit] NOT NULL,
	[ID_User_Status] [int] NOT NULL,
	[Personal_Account] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Abonent] PRIMARY KEY CLUSTERED 
(
	[ID_Abonent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Station]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Station](
	[ID_Station] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Cost] [decimal](10, 2) NOT NULL,
	[ID_ATS] [int] NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Street] [varchar](50) NOT NULL,
	[House] [varchar](4) NOT NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[ID_Station] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Status]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Status](
	[ID_User_Status] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User_Status] PRIMARY KEY CLUSTERED 
(
	[ID_User_Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Abonents]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Abonents]
AS
SELECT dbo.Abonent.ID_Abonent, dbo.Abonent.Surname + ' ' + dbo.Abonent.Name + ' ' + dbo.Abonent.LastName AS ФИО, dbo.Abonent.Phone_Number AS [Номер телефона], 
             'Индекс:' + dbo.Abonent.Index_ + ', г.' + dbo.Abonent.City + ', ул.' + dbo.Abonent.Street + ' д.' + dbo.Abonent.House + ' кв.' + dbo.Abonent.Flat + '.' AS Адрес, dbo.Abonent.Personal_Account AS Счёт, dbo.Abonent.Benefit AS Льгота, dbo.Abonent.Cross_City AS Межгород, 
             dbo.User_Status.Name AS Статус
FROM   dbo.Abonent INNER JOIN
             dbo.Station ON dbo.Abonent.ID_Station = dbo.Station.ID_Station INNER JOIN
             dbo.User_Status ON dbo.Abonent.ID_User_Status = dbo.User_Status.ID_User_Status
GO
/****** Object:  View [dbo].[StationCost1]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StationCost1]
AS
SELECT ID_Station, Name + ' ' + CAST(Cost AS varchar) + 'р. в м.' AS f
FROM   dbo.Station
WHERE (ID_ATS = 1)
GO
/****** Object:  Table [dbo].[ATS]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ATS](
	[ID_ATS] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ATS] PRIMARY KEY CLUSTERED 
(
	[ID_ATS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewStation]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewStation]
AS
SELECT dbo.Station.ID_Station, dbo.Station.Name AS Название, dbo.Station.Cost AS Стоимость, dbo.ATS.Name AS АТС, dbo.Station.City AS Город, dbo.Station.Street AS Улица, dbo.Station.House AS Дом, dbo.Station.ID_ATS
FROM   dbo.Station INNER JOIN
             dbo.ATS ON dbo.Station.ID_ATS = dbo.ATS.ID_ATS
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[ID_Payment] [int] IDENTITY(1,1) NOT NULL,
	[ID_Abonent] [int] NULL,
	[ID_Organization] [int] NULL,
	[Cost] [decimal](5, 2) NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[ID_Payment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewPaymentAbonent]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewPaymentAbonent]
AS
SELECT dbo.Payment.ID_Abonent, dbo.Abonent.Phone_Number AS [Номер телефона], dbo.Payment.Cost AS Сумма
FROM   dbo.Abonent INNER JOIN
             dbo.Payment ON dbo.Abonent.ID_Abonent = dbo.Payment.ID_Abonent
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[ID_Organization] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Phone_Number] [varchar](16) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Index_] [varchar](6) NULL,
	[City] [varchar](50) NOT NULL,
	[Street] [varchar](50) NOT NULL,
	[House] [varchar](4) NOT NULL,
	[Personal_Account] [decimal](10, 2) NOT NULL,
	[ID_Station] [int] NOT NULL,
	[ID_User_Status] [int] NOT NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[ID_Organization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewPaymentOrg]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewPaymentOrg]
AS
SELECT dbo.Payment.ID_Payment, dbo.Organization.Phone_Number AS [Номер телефона], dbo.Payment.Cost AS Сумма
FROM   dbo.Organization INNER JOIN
             dbo.Payment ON dbo.Organization.ID_Organization = dbo.Payment.ID_Organization
GO
/****** Object:  View [dbo].[StationCost23]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StationCost23]
AS
SELECT ID_Station, Name + ' ' + CAST(Cost AS varchar) + 'р. в м.' AS f
FROM   dbo.Station
WHERE (ID_ATS = 2) OR
             (ID_ATS = 3)
GO
/****** Object:  View [dbo].[Organizations]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Organizations]
AS
SELECT dbo.Organization.ID_Organization, dbo.Organization.Name AS Название, dbo.Organization.Phone_Number AS [Номер телефона], dbo.Organization.Personal_Account AS Счёт, dbo.User_Status.Name AS Статус, dbo.Station.Name + ' ' + CAST(dbo.Station.Cost AS varchar) 
             + 'р. в м.' AS Станция
FROM   dbo.Organization INNER JOIN
             dbo.User_Status ON dbo.Organization.ID_User_Status = dbo.User_Status.ID_User_Status INNER JOIN
             dbo.Station ON dbo.Organization.ID_Station = dbo.Station.ID_Station
GO
/****** Object:  Table [dbo].[Otdel]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Otdel](
	[ID_Otdel] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Plus_Number] [varchar](4) NOT NULL,
	[ID_Organization] [int] NOT NULL,
 CONSTRAINT [PK_Otdel] PRIMARY KEY CLUSTERED 
(
	[ID_Otdel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History_Calls_Organization]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History_Calls_Organization](
	[ID_History_Calls_Organization] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[ID_Otdel] [int] NOT NULL,
	[ID_Call_Status] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID_History_Calls_Organization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Call_Status]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Call_Status](
	[ID_Call_Status] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Call_Status] PRIMARY KEY CLUSTERED 
(
	[ID_Call_Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewHistoryOrg]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewHistoryOrg]
AS
SELECT dbo.Organization.ID_Organization, dbo.Otdel.Name AS Исходящий, dbo.History_Calls_Organization.Date AS [Дата звонка], dbo.Call_Status.Name AS [Статус звонка]
FROM   dbo.Call_Status INNER JOIN
             dbo.History_Calls_Organization ON dbo.Call_Status.ID_Call_Status = dbo.History_Calls_Organization.ID_Call_Status INNER JOIN
             dbo.Otdel ON dbo.History_Calls_Organization.ID_Otdel = dbo.Otdel.ID_Otdel INNER JOIN
             dbo.Organization ON dbo.Otdel.ID_Organization = dbo.Organization.ID_Organization
GO
/****** Object:  View [dbo].[ViewOtdel]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewOtdel]
AS
SELECT ID_Otdel, Name AS Название, Plus_Number AS [Добавочный номер], ID_Organization
FROM   dbo.Otdel
GO
/****** Object:  View [dbo].[ViewContactsAb]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewContactsAb]
AS
SELECT ID_Abonent, Street, Surname + ' ' + Name + ' ' + LastName AS ФИО, Phone_Number AS [Номер телефона], City AS Город
FROM   dbo.Abonent
GO
/****** Object:  View [dbo].[ViewContactsOrg]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewContactsOrg]
AS
SELECT dbo.Organization.ID_Organization, dbo.Otdel.ID_Otdel, dbo.Otdel.Name AS [Название отдела], dbo.Otdel.Plus_Number AS [Добавочный номер]
FROM   dbo.Organization INNER JOIN
             dbo.Otdel ON dbo.Organization.ID_Organization = dbo.Otdel.ID_Organization
GO
/****** Object:  Table [dbo].[History_Calls]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History_Calls](
	[ID_History_Calls] [int] IDENTITY(1,1) NOT NULL,
	[ID_Abonent_1] [int] NOT NULL,
	[ID_Abonent_2] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[ID_Call_Status] [int] NOT NULL,
 CONSTRAINT [PK_History_Calls] PRIMARY KEY CLUSTERED 
(
	[ID_History_Calls] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operator]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operator](
	[ID_Operator] [int] IDENTITY(1,1) NOT NULL,
	[Phone_Number] [varchar](16) NOT NULL,
	[Password] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Operator] PRIMARY KEY CLUSTERED 
(
	[ID_Operator] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sex]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sex](
	[ID_Sex] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1) NOT NULL,
 CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED 
(
	[ID_Sex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Abonent]  WITH CHECK ADD  CONSTRAINT [FK_Abonent_Abonent_Sex] FOREIGN KEY([ID_Sex])
REFERENCES [dbo].[Sex] ([ID_Sex])
GO
ALTER TABLE [dbo].[Abonent] CHECK CONSTRAINT [FK_Abonent_Abonent_Sex]
GO
ALTER TABLE [dbo].[Abonent]  WITH CHECK ADD  CONSTRAINT [FK_Abonent_Abonent_Station] FOREIGN KEY([ID_Station])
REFERENCES [dbo].[Station] ([ID_Station])
GO
ALTER TABLE [dbo].[Abonent] CHECK CONSTRAINT [FK_Abonent_Abonent_Station]
GO
ALTER TABLE [dbo].[Abonent]  WITH CHECK ADD  CONSTRAINT [FK_Abonent_Abonent_UserStatus] FOREIGN KEY([ID_User_Status])
REFERENCES [dbo].[User_Status] ([ID_User_Status])
GO
ALTER TABLE [dbo].[Abonent] CHECK CONSTRAINT [FK_Abonent_Abonent_UserStatus]
GO
ALTER TABLE [dbo].[History_Calls]  WITH CHECK ADD  CONSTRAINT [FK_History_Calls_History_Calls_Abonent1] FOREIGN KEY([ID_Abonent_1])
REFERENCES [dbo].[Abonent] ([ID_Abonent])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[History_Calls] CHECK CONSTRAINT [FK_History_Calls_History_Calls_Abonent1]
GO
ALTER TABLE [dbo].[History_Calls]  WITH CHECK ADD  CONSTRAINT [FK_History_Calls_History_Calls_Abonent2] FOREIGN KEY([ID_Abonent_2])
REFERENCES [dbo].[Abonent] ([ID_Abonent])
GO
ALTER TABLE [dbo].[History_Calls] CHECK CONSTRAINT [FK_History_Calls_History_Calls_Abonent2]
GO
ALTER TABLE [dbo].[History_Calls]  WITH CHECK ADD  CONSTRAINT [FK_History_Calls_History_Calls_CallStatus] FOREIGN KEY([ID_Call_Status])
REFERENCES [dbo].[Call_Status] ([ID_Call_Status])
GO
ALTER TABLE [dbo].[History_Calls] CHECK CONSTRAINT [FK_History_Calls_History_Calls_CallStatus]
GO
ALTER TABLE [dbo].[History_Calls_Organization]  WITH CHECK ADD  CONSTRAINT [FK_History_Calls_Organization_History_Calls_Organization_Otdel] FOREIGN KEY([ID_Otdel])
REFERENCES [dbo].[Otdel] ([ID_Otdel])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[History_Calls_Organization] CHECK CONSTRAINT [FK_History_Calls_Organization_History_Calls_Organization_Otdel]
GO
ALTER TABLE [dbo].[History_Calls_Organization]  WITH CHECK ADD  CONSTRAINT [FK_History_Calls_Organization_History_Calls_Organization_Status] FOREIGN KEY([ID_Call_Status])
REFERENCES [dbo].[Call_Status] ([ID_Call_Status])
GO
ALTER TABLE [dbo].[History_Calls_Organization] CHECK CONSTRAINT [FK_History_Calls_Organization_History_Calls_Organization_Status]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_Organization_Station] FOREIGN KEY([ID_Station])
REFERENCES [dbo].[Station] ([ID_Station])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_Organization_Station]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_Organization_UserStatus] FOREIGN KEY([ID_User_Status])
REFERENCES [dbo].[User_Status] ([ID_User_Status])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_Organization_UserStatus]
GO
ALTER TABLE [dbo].[Otdel]  WITH CHECK ADD  CONSTRAINT [FK_Otdel_Organization] FOREIGN KEY([ID_Organization])
REFERENCES [dbo].[Organization] ([ID_Organization])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Otdel] CHECK CONSTRAINT [FK_Otdel_Organization]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Payment_Abonent] FOREIGN KEY([ID_Abonent])
REFERENCES [dbo].[Abonent] ([ID_Abonent])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Payment_Abonent]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Payment_Organization] FOREIGN KEY([ID_Organization])
REFERENCES [dbo].[Organization] ([ID_Organization])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Payment_Organization]
GO
ALTER TABLE [dbo].[Station]  WITH CHECK ADD  CONSTRAINT [FK_Station_Station_ATS] FOREIGN KEY([ID_ATS])
REFERENCES [dbo].[ATS] ([ID_ATS])
GO
ALTER TABLE [dbo].[Station] CHECK CONSTRAINT [FK_Station_Station_ATS]
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuery]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteQuery]
(
	@ID_Station int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[Station] WHERE ([ID_Station] = @ID_Station)
GO
/****** Object:  StoredProcedure [dbo].[FillByid]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FillByid]
(
	@ID_Abonent int
)
AS
	SET NOCOUNT ON;
SELECT ID_History_Calls, ID_Abonent_1, ID_Abonent_2, Date, ID_Call_Status FROM dbo.History_Calls
where (ID_Abonent_1 = @ID_Abonent) and (ID_Abonent_2 = @ID_Abonent) 
GO
/****** Object:  StoredProcedure [dbo].[SelectQuery]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectQuery]
(
	@ID int
)
AS
	SET NOCOUNT ON;
SELECT ID_History_Calls, ID_Abonent_1, ID_Abonent_2, Date, ID_Call_Status FROM dbo.History_Calls
Where (ID_Abonent_1 = @ID) OR (ID_Abonent_2 = @ID)
GO
/****** Object:  StoredProcedure [dbo].[UpdatePayment]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePayment]
(
	@NeedToPay decimal(10, 2),
	@ID_User_Status int,
	@ID_Abonent int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[Abonent] 
SET [Personal_Account] = [Personal_Account] - @NeedToPay, [ID_User_Status] = @ID_User_Status
FROM dbo.Abonent INNER JOIN dbo.Station ON dbo.Abonent.ID_Station = dbo.Station.ID_Station
WHERE ([ID_Abonent] = @ID_Abonent) 
GO
/****** Object:  StoredProcedure [dbo].[UpdatePaymentOrg]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePaymentOrg]
(
	@NeedToPay decimal(10, 2),
	@ID_User_Status int,
	@ID_Org int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[Organization]
SET [Personal_Account] = [Personal_Account] - @NeedToPay, [ID_User_Status] = @ID_User_Status
FROM dbo.[Organization] INNER JOIN dbo.Station ON dbo.Organization.ID_Station = dbo.Station.ID_Station
WHERE ([ID_Organization] = @ID_Org) 
GO
/****** Object:  StoredProcedure [dbo].[UpdateSum]    Script Date: 21.04.2022 19:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSum]
(
	@PaySum decimal(10, 2),
	@ID_Abonent int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[Abonent]  SET [Personal_Account] = [Personal_Account] + @PaySum 
WHERE ([ID_Abonent] = @ID_Abonent)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[65] 4[3] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Abonent"
            Begin Extent = 
               Top = 26
               Left = 398
               Bottom = 346
               Right = 679
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "Station"
            Begin Extent = 
               Top = 17
               Left = 105
               Bottom = 214
               Right = 333
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "User_Status"
            Begin Extent = 
               Top = 18
               Left = 745
               Bottom = 161
               Right = 973
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 2970
         Width = 1740
         Width = 4410
         Width = 610
         Width = 760
         Width = 1130
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Abonents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Abonents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Organization"
            Begin Extent = 
               Top = 22
               Left = 566
               Bottom = 219
               Right = 809
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "User_Status"
            Begin Extent = 
               Top = 10
               Left = 310
               Bottom = 153
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Station"
            Begin Extent = 
               Top = 30
               Left = 970
               Bottom = 227
               Right = 1198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1380
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 3110
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Organizations'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Organizations'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Station"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 285
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StationCost1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StationCost1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Station"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 285
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 2880
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StationCost23'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StationCost23'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Abonent"
            Begin Extent = 
               Top = 13
               Left = 346
               Bottom = 210
               Right = 589
            End
            DisplayFlags = 280
            TopColumn = 10
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 3090
         Width = 1390
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewContactsAb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewContactsAb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Organization"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 300
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Otdel"
            Begin Extent = 
               Top = 9
               Left = 357
               Bottom = 206
               Right = 588
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewContactsOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewContactsOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Call_Status"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 152
               Right = 285
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "History_Calls_Organization"
            Begin Extent = 
               Top = 9
               Left = 342
               Bottom = 206
               Right = 680
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Organization"
            Begin Extent = 
               Top = 44
               Left = 1206
               Bottom = 241
               Right = 1449
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Otdel"
            Begin Extent = 
               Top = 6
               Left = 784
               Bottom = 203
               Right = 1015
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1000
         Width = 1620
         Width = 1470
         Width = 1880
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewHistoryOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewHistoryOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewHistoryOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Otdel"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 288
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewOtdel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewOtdel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Abonent"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 300
            End
            DisplayFlags = 280
            TopColumn = 14
         End
         Begin Table = "Payment"
            Begin Extent = 
               Top = 9
               Left = 357
               Bottom = 206
               Right = 588
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1890
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPaymentAbonent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPaymentAbonent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Payment"
            Begin Extent = 
               Top = 9
               Left = 357
               Bottom = 206
               Right = 588
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Organization"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 300
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1190
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPaymentOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPaymentOrg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Station"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 285
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "ATS"
            Begin Extent = 
               Top = 9
               Left = 342
               Bottom = 152
               Right = 570
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1210
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewStation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewStation'
GO
USE [master]
GO
ALTER DATABASE [CityTelephony] SET  READ_WRITE 
GO
