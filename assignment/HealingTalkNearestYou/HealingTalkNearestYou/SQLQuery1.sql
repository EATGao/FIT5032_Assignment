
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/09/2022 17:33:33
-- Generated from EDMX file: D:\fit5032\FIT5032_Assignment\assignment\HealingTalkNearestYou\HealingTalkNearestYou\HealingTalkNearestYou\Models\HTNY_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HTNY_Db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CounsellingSet'
CREATE TABLE [dbo].[CounsellingSet] (
    [CounsellingId] int IDENTITY(1,1) NOT NULL,
    [CounsellingDateTime] datetime  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [PatientUserId] int  NULL,
    [PyschologistUserId] int  NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSet_Patient'
CREATE TABLE [dbo].[UserSet_Patient] (
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserSet_Pyschologist'
CREATE TABLE [dbo].[UserSet_Pyschologist] (
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [ProfileDescription] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'UserSet_Admin'
CREATE TABLE [dbo].[UserSet_Admin] (
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CounsellingId] in table 'CounsellingSet'
ALTER TABLE [dbo].[CounsellingSet]
ADD CONSTRAINT [PK_CounsellingSet]
    PRIMARY KEY CLUSTERED ([CounsellingId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserSet_Patient'
ALTER TABLE [dbo].[UserSet_Patient]
ADD CONSTRAINT [PK_UserSet_Patient]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserSet_Pyschologist'
ALTER TABLE [dbo].[UserSet_Pyschologist]
ADD CONSTRAINT [PK_UserSet_Pyschologist]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserSet_Admin'
ALTER TABLE [dbo].[UserSet_Admin]
ADD CONSTRAINT [PK_UserSet_Admin]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PatientUserId] in table 'CounsellingSet'
ALTER TABLE [dbo].[CounsellingSet]
ADD CONSTRAINT [FK_PatientCounselling]
    FOREIGN KEY ([PatientUserId])
    REFERENCES [dbo].[UserSet_Patient]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientCounselling'
CREATE INDEX [IX_FK_PatientCounselling]
ON [dbo].[CounsellingSet]
    ([PatientUserId]);
GO

-- Creating foreign key on [PyschologistUserId] in table 'CounsellingSet'
ALTER TABLE [dbo].[CounsellingSet]
ADD CONSTRAINT [FK_PyschologistCounselling]
    FOREIGN KEY ([PyschologistUserId])
    REFERENCES [dbo].[UserSet_Pyschologist]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PyschologistCounselling'
CREATE INDEX [IX_FK_PyschologistCounselling]
ON [dbo].[CounsellingSet]
    ([PyschologistUserId]);
GO

-- Creating foreign key on [UserId] in table 'UserSet_Patient'
ALTER TABLE [dbo].[UserSet_Patient]
ADD CONSTRAINT [FK_Patient_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'UserSet_Pyschologist'
ALTER TABLE [dbo].[UserSet_Pyschologist]
ADD CONSTRAINT [FK_Pyschologist_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'UserSet_Admin'
ALTER TABLE [dbo].[UserSet_Admin]
ADD CONSTRAINT [FK_Admin_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------