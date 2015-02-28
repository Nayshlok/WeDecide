
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/27/2015 18:32:38
-- Generated from EDMX file: C:\Users\David Borland\Source\Repos\WeDecide\WeDecide\WeDecide\Models\Concrete\Question.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WeDecide2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[FK_QuestionResponse_Question]', 'F') IS NOT NULL
    ALTER TABLE [WeDecide2ModelStoreContainer].[QuestionResponse] DROP CONSTRAINT [FK_QuestionResponse_Question];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[FK_QuestionResponse_Response]', 'F') IS NOT NULL
    ALTER TABLE [WeDecide2ModelStoreContainer].[QuestionResponse] DROP CONSTRAINT [FK_QuestionResponse_Response];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[FK_UserQuestion_Question]', 'F') IS NOT NULL
    ALTER TABLE [WeDecide2ModelStoreContainer].[UserQuestion] DROP CONSTRAINT [FK_UserQuestion_Question];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[FK_UserQuestion_User]', 'F') IS NOT NULL
    ALTER TABLE [WeDecide2ModelStoreContainer].[UserQuestion] DROP CONSTRAINT [FK_UserQuestion_User];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[FK_UserResponse_Response]', 'F') IS NOT NULL
    ALTER TABLE [WeDecide2ModelStoreContainer].[UserResponse] DROP CONSTRAINT [FK_UserResponse_Response];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[FK_UserResponse_User]', 'F') IS NOT NULL
    ALTER TABLE [WeDecide2ModelStoreContainer].[UserResponse] DROP CONSTRAINT [FK_UserResponse_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Question]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Question];
GO
IF OBJECT_ID(N'[dbo].[Response]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Response];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[QuestionResponse]', 'U') IS NOT NULL
    DROP TABLE [WeDecide2ModelStoreContainer].[QuestionResponse];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[UserQuestion]', 'U') IS NOT NULL
    DROP TABLE [WeDecide2ModelStoreContainer].[UserQuestion];
GO
IF OBJECT_ID(N'[WeDecide2ModelStoreContainer].[UserResponse]', 'U') IS NOT NULL
    DROP TABLE [WeDecide2ModelStoreContainer].[UserResponse];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(268)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [FreeResponseEnabled] bit  NOT NULL,
    [Users_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Responses'
CREATE TABLE [dbo].[Responses] (
    [Id] int  NOT NULL,
    [text] nvarchar(128)  NOT NULL,
    [Questions_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'UserResponse'
CREATE TABLE [dbo].[UserResponse] (
    [Responses_Id] int  NOT NULL,
    [Users_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [PK_Responses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Responses_Id], [Users_Id] in table 'UserResponse'
ALTER TABLE [dbo].[UserResponse]
ADD CONSTRAINT [PK_UserResponse]
    PRIMARY KEY CLUSTERED ([Responses_Id], [Users_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Questions_Id] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_QuestionResponse]
    FOREIGN KEY ([Questions_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionResponse'
CREATE INDEX [IX_FK_QuestionResponse]
ON [dbo].[Responses]
    ([Questions_Id]);
GO

-- Creating foreign key on [Users_Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_UserQuestion]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserQuestion'
CREATE INDEX [IX_FK_UserQuestion]
ON [dbo].[Questions]
    ([Users_Id]);
GO

-- Creating foreign key on [Responses_Id] in table 'UserResponse'
ALTER TABLE [dbo].[UserResponse]
ADD CONSTRAINT [FK_UserResponse_Response]
    FOREIGN KEY ([Responses_Id])
    REFERENCES [dbo].[Responses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'UserResponse'
ALTER TABLE [dbo].[UserResponse]
ADD CONSTRAINT [FK_UserResponse_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserResponse_User'
CREATE INDEX [IX_FK_UserResponse_User]
ON [dbo].[UserResponse]
    ([Users_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------