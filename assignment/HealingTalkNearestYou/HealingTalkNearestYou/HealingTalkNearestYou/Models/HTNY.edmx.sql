
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/29/2022 23:05:07
-- Generated from EDMX file: D:\fit5032\FIT5032_Assignment\assignment\HealingTalkNearestYou\HealingTalkNearestYou\HealingTalkNearestYou\Models\HTNY.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HTNYDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PsychologistCounselling]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CounsellingSet] DROP CONSTRAINT [FK_PsychologistCounselling];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientCounselling]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CounsellingSet] DROP CONSTRAINT [FK_PatientCounselling];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AdminSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdminSet];
GO
IF OBJECT_ID(N'[dbo].[PsychologistSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PsychologistSet];
GO
IF OBJECT_ID(N'[dbo].[CounsellingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CounsellingSet];
GO
IF OBJECT_ID(N'[dbo].[PatientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AdminSet'
CREATE TABLE [dbo].[AdminSet] (
    [AdminId] int IDENTITY(1,1) NOT NULL,
    [AdminEmail] nvarchar(max)  NOT NULL,
    [AdminPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PsychologistSet'
CREATE TABLE [dbo].[PsychologistSet] (
    [PsyId] int IDENTITY(1,1) NOT NULL,
    [PsyEmail] nvarchar(max)  NOT NULL,
    [PsyPassword] nvarchar(max)  NOT NULL,
    [PsyFirstName] nvarchar(max)  NOT NULL,
    [PsyLastName] nvarchar(max)  NOT NULL,
    [PsyGender] nvarchar(max)  NOT NULL,
    [PsyDOB] datetime  NULL,
    [PsyDescription] nvarchar(max)  NULL
);
GO

-- Creating table 'CounsellingSet'
CREATE TABLE [dbo].[CounsellingSet] (
    [CId] int IDENTITY(1,1) NOT NULL,
    [CDateTime] datetime  NOT NULL,
    [CRate] int  NULL,
    [CStatus] nvarchar(max)  NOT NULL,
    [PsychologistPsyId] int  NOT NULL,
    [PatientPatientId] int  NULL
);
GO

-- Creating table 'PatientSet'
CREATE TABLE [dbo].[PatientSet] (
    [PatientId] int IDENTITY(1,1) NOT NULL,
    [PatientEmail] nvarchar(max)  NOT NULL,
    [PatientPassword] nvarchar(max)  NOT NULL,
    [PatientFirstName] nvarchar(max)  NOT NULL,
    [PatientLastName] nvarchar(max)  NOT NULL,
    [PatientGender] nvarchar(max)  NOT NULL,
    [PatientDOB] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AdminId] in table 'AdminSet'
ALTER TABLE [dbo].[AdminSet]
ADD CONSTRAINT [PK_AdminSet]
    PRIMARY KEY CLUSTERED ([AdminId] ASC);
GO

-- Creating primary key on [PsyId] in table 'PsychologistSet'
ALTER TABLE [dbo].[PsychologistSet]
ADD CONSTRAINT [PK_PsychologistSet]
    PRIMARY KEY CLUSTERED ([PsyId] ASC);
GO

-- Creating primary key on [CId] in table 'CounsellingSet'
ALTER TABLE [dbo].[CounsellingSet]
ADD CONSTRAINT [PK_CounsellingSet]
    PRIMARY KEY CLUSTERED ([CId] ASC);
GO

-- Creating primary key on [PatientId] in table 'PatientSet'
ALTER TABLE [dbo].[PatientSet]
ADD CONSTRAINT [PK_PatientSet]
    PRIMARY KEY CLUSTERED ([PatientId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PsychologistPsyId] in table 'CounsellingSet'
ALTER TABLE [dbo].[CounsellingSet]
ADD CONSTRAINT [FK_PsychologistCounselling]
    FOREIGN KEY ([PsychologistPsyId])
    REFERENCES [dbo].[PsychologistSet]
        ([PsyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PsychologistCounselling'
CREATE INDEX [IX_FK_PsychologistCounselling]
ON [dbo].[CounsellingSet]
    ([PsychologistPsyId]);
GO

-- Creating foreign key on [PatientPatientId] in table 'CounsellingSet'
ALTER TABLE [dbo].[CounsellingSet]
ADD CONSTRAINT [FK_PatientCounselling]
    FOREIGN KEY ([PatientPatientId])
    REFERENCES [dbo].[PatientSet]
        ([PatientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientCounselling'
CREATE INDEX [IX_FK_PatientCounselling]
ON [dbo].[CounsellingSet]
    ([PatientPatientId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------