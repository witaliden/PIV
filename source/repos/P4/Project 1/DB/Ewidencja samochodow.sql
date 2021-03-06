USE [master]
GO
/****** Object:  Database [Ewidencja samochodow]    Script Date: 29.07.2022 10:11:29 ******/
CREATE DATABASE [Ewidencja samochodow]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ewidencja samochodow', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Ewidencja samochodow.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Ewidencja samochodow_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Ewidencja samochodow_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Ewidencja samochodow] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ewidencja samochodow].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ewidencja samochodow] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ewidencja samochodow] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ewidencja samochodow] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Ewidencja samochodow] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ewidencja samochodow] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Ewidencja samochodow] SET  MULTI_USER 
GO
ALTER DATABASE [Ewidencja samochodow] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ewidencja samochodow] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ewidencja samochodow] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ewidencja samochodow] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Ewidencja samochodow] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Ewidencja samochodow] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Ewidencja samochodow] SET QUERY_STORE = OFF
GO
USE [Ewidencja samochodow]
GO
/****** Object:  Table [dbo].[Samochody]    Script Date: 29.07.2022 10:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Samochody](
	[VIN] [char](17) NOT NULL,
	[Dostepnosc] [char](20) NULL,
	[NumerRejestracyjny] [char](10) NOT NULL,
	[Marka] [char](20) NULL,
	[Model] [char](20) NULL,
	[PojemnoscSilnika] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[VIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DostepneSamochody]    Script Date: 29.07.2022 10:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[DostepneSamochody] AS
SELECT (Marka + ' ' + Model + ' (' + NumerRejestracyjny +')') AS [Samochód]
FROM Samochody
WHERE Dostepnosc = 'Dostępny'
GO
/****** Object:  Table [dbo].[Pracownicy]    Script Date: 29.07.2022 10:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pracownicy](
	[PracownikID] [int] IDENTITY(1,1) NOT NULL,
	[PESEL] [char](11) NOT NULL,
	[Imie] [char](30) NOT NULL,
	[Nazwisko] [char](40) NOT NULL,
	[Plec] [char](1) NULL,
	[Stanowisko] [char](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PracownikID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Przejazdy]    Script Date: 29.07.2022 10:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Przejazdy](
	[PrzejazdID] [int] IDENTITY(1,1) NOT NULL,
	[VIN] [char](17) NOT NULL,
	[PracownikID] [int] NOT NULL,
	[CelWyjazdu] [varchar](90) NULL,
	[DataCzasOdbioru] [datetime] NOT NULL,
	[DataCzasZwrotu] [datetime] NULL,
	[StanLicznikaPrzed] [int] NOT NULL,
	[StanLicznikaPo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PrzejazdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[wTrkaciePrzejazdu]    Script Date: 29.07.2022 10:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[wTrkaciePrzejazdu] AS
SELECT (p.Imie + ' ' + p.Nazwisko + ' (' + Stanowisko + ')') AS Pracownik,
	 pr.CelWyjazdu AS [Cel wyjazdu], 
	(s.Marka + ' ' + s.Model + ' ' + s.NumerRejestracyjny) AS Samochód
FROM Przejazdy AS pr, Samochody AS s, Pracownicy as p
WHERE s.VIN = pr.VIN AND p.PracownikID = pr.PracownikID	 
AND pr.DataCzasZwrotu IS NULL
GO
ALTER TABLE [dbo].[Przejazdy]  WITH CHECK ADD  CONSTRAINT [fk_PracownikID] FOREIGN KEY([PracownikID])
REFERENCES [dbo].[Pracownicy] ([PracownikID])
GO
ALTER TABLE [dbo].[Przejazdy] CHECK CONSTRAINT [fk_PracownikID]
GO
ALTER TABLE [dbo].[Przejazdy]  WITH CHECK ADD  CONSTRAINT [fk_VIN] FOREIGN KEY([VIN])
REFERENCES [dbo].[Samochody] ([VIN])
GO
ALTER TABLE [dbo].[Przejazdy] CHECK CONSTRAINT [fk_VIN]
GO
ALTER TABLE [dbo].[Pracownicy]  WITH CHECK ADD  CONSTRAINT [checkPlec] CHECK  (([Plec]='K' OR [Plec]='M'))
GO
ALTER TABLE [dbo].[Pracownicy] CHECK CONSTRAINT [checkPlec]
GO
ALTER TABLE [dbo].[Przejazdy]  WITH CHECK ADD  CONSTRAINT [CK_DataCzasZwrotu] CHECK  (([DataCzasZwrotu]>=[DataCzasOdbioru]))
GO
ALTER TABLE [dbo].[Przejazdy] CHECK CONSTRAINT [CK_DataCzasZwrotu]
GO
ALTER TABLE [dbo].[Samochody]  WITH CHECK ADD  CONSTRAINT [checkDostepnosc] CHECK  (([Dostepnosc]='Archiwalny' OR [Dostepnosc]='Wypożyczony' OR [Dostepnosc]='Dostępny'))
GO
ALTER TABLE [dbo].[Samochody] CHECK CONSTRAINT [checkDostepnosc]
GO
USE [master]
GO
ALTER DATABASE [Ewidencja samochodow] SET  READ_WRITE 
GO
