USE [master]
GO

ALTER DATABASE [TableDependencyDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
DROP DATABASE [TableDependencyDB]
GO
--EXEC sp_detach_db [TableDependencyDB], 'true';
CREATE DATABASE [TableDependencyDB]
GO

USE [TableDependencyDB]
GO

ALTER DATABASE [TableDependencyDB] SET ENABLE_BROKER;
GO

DROP TABLE [Stocks]
GO
CREATE TABLE [dbo].[Stocks](
    [Code] [nvarchar](50) PRIMARY KEY,
    [Name] [nvarchar](50) NULL,
    [Price] [decimal](18, 0) NULL
) ON [PRIMARY]
GO