﻿/*
Deployment script for DBCarros

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "DBCarros"
:setvar DefaultFilePrefix "DBCarros"
:setvar DefaultDataPath "C:\Users\logonrmlocal\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\logonrmlocal\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key cc0c7f1b-12d8-4ab0-9dc0-dc4eeb4d83c5 is skipped, element [dbo].[Carro].[Marca] (SqlSimpleColumn) will not be renamed to MarcaId';


GO
PRINT N'Creating [dbo].[Carro]...';


GO
CREATE TABLE [dbo].[Carro] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [MarcaId]     INT           NOT NULL,
    [Ano]         INT           NULL,
    [Esportivo]   BIT           NULL,
    [Placa]       VARCHAR (8)   NOT NULL,
    [Combustivel] INT           NOT NULL,
    [Descricao]   VARCHAR (150) NULL,
    [Renavam]     INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Documento]...';


GO
CREATE TABLE [dbo].[Documento] (
    [Renavam]        INT  NOT NULL,
    [Categoria]      INT  NOT NULL,
    [DataFabricacao] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([Renavam] ASC)
);


GO
PRINT N'Creating [dbo].[Marca]...';


GO
CREATE TABLE [dbo].[Marca] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (50) NOT NULL,
    [DataCriacao] DATETIME     NOT NULL,
    [CNPJ]        VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Carro_Documento]...';


GO
ALTER TABLE [dbo].[Carro] WITH NOCHECK
    ADD CONSTRAINT [FK_Carro_Documento] FOREIGN KEY ([Renavam]) REFERENCES [dbo].[Documento] ([Renavam]);


GO
PRINT N'Creating [dbo].[FK_Carro_Marca]...';


GO
ALTER TABLE [dbo].[Carro] WITH NOCHECK
    ADD CONSTRAINT [FK_Carro_Marca] FOREIGN KEY ([MarcaId]) REFERENCES [dbo].[Marca] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'cc0c7f1b-12d8-4ab0-9dc0-dc4eeb4d83c5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('cc0c7f1b-12d8-4ab0-9dc0-dc4eeb4d83c5')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Carro] WITH CHECK CHECK CONSTRAINT [FK_Carro_Documento];

ALTER TABLE [dbo].[Carro] WITH CHECK CHECK CONSTRAINT [FK_Carro_Marca];


GO
PRINT N'Update complete.';


GO
