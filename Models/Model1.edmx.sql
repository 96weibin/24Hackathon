
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/05/2022 22:20:48
-- Generated from EDMX file: F:\New folder\AIQuestionAnswer\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [KnowledgeBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CommentPerson_Comment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentPerson] DROP CONSTRAINT [FK_CommentPerson_Comment];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentPerson_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentPerson] DROP CONSTRAINT [FK_CommentPerson_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentPerson1_Comment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentPerson1] DROP CONSTRAINT [FK_CommentPerson1_Comment];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentPerson1_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentPerson1] DROP CONSTRAINT [FK_CommentPerson1_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PersonComment];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_QuestionPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_QuestionComment];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentComment];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPersonRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People] DROP CONSTRAINT [FK_PersonPersonRole];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionPerson1_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionPerson1] DROP CONSTRAINT [FK_QuestionPerson1_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionPerson1_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionPerson1] DROP CONSTRAINT [FK_QuestionPerson1_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chats] DROP CONSTRAINT [FK_ChatPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_FamilyProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_FamilyProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionPerson2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_QuestionPerson2];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_QuestionProduct];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[PersonRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonRoles];
GO
IF OBJECT_ID(N'[dbo].[Chats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chats];
GO
IF OBJECT_ID(N'[dbo].[Families]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Families];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[CommentPerson]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentPerson];
GO
IF OBJECT_ID(N'[dbo].[CommentPerson1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentPerson1];
GO
IF OBJECT_ID(N'[dbo].[QuestionPerson1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionPerson1];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [LikeNumber] int  NOT NULL,
    [CreateDate] datetime  NULL,
    [Summary] nvarchar(max)  NULL,
    [State] nvarchar(max)  NOT NULL,
    [IsPrivate] bit  NOT NULL,
    [Person_Id] int  NOT NULL,
    [Supportor_Id] int  NOT NULL,
    [Product_Id] int  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RoleId] int  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [LikeNumber] int  NULL,
    [CreateDate] datetime  NULL,
    [ParentCommentId] int  NULL,
    [QuestionId] int  NOT NULL,
    [IsRefer] bit  NOT NULL,
    [Person_Id] int  NOT NULL,
    [Question_Id] int  NOT NULL
);
GO

-- Creating table 'PersonRoles'
CREATE TABLE [dbo].[PersonRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Chats'
CREATE TABLE [dbo].[Chats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsQuestion] bit  NOT NULL,
    [TimeStamp] datetime  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Person_Id] int  NULL
);
GO

-- Creating table 'Families'
CREATE TABLE [dbo].[Families] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FamilyId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Supportor_Id] int  NOT NULL
);
GO

-- Creating table 'CommentPerson'
CREATE TABLE [dbo].[CommentPerson] (
    [LikeComments_Id] int  NOT NULL,
    [PeopleLike_Id] int  NOT NULL
);
GO

-- Creating table 'CommentPerson1'
CREATE TABLE [dbo].[CommentPerson1] (
    [DislikeComments_Id] int  NOT NULL,
    [PeopleDislike_Id] int  NOT NULL
);
GO

-- Creating table 'QuestionPerson1'
CREATE TABLE [dbo].[QuestionPerson1] (
    [ReferQuestions_Id] int  NOT NULL,
    [ReferPeople_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonRoles'
ALTER TABLE [dbo].[PersonRoles]
ADD CONSTRAINT [PK_PersonRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [PK_Chats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Families'
ALTER TABLE [dbo].[Families]
ADD CONSTRAINT [PK_Families]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LikeComments_Id], [PeopleLike_Id] in table 'CommentPerson'
ALTER TABLE [dbo].[CommentPerson]
ADD CONSTRAINT [PK_CommentPerson]
    PRIMARY KEY CLUSTERED ([LikeComments_Id], [PeopleLike_Id] ASC);
GO

-- Creating primary key on [DislikeComments_Id], [PeopleDislike_Id] in table 'CommentPerson1'
ALTER TABLE [dbo].[CommentPerson1]
ADD CONSTRAINT [PK_CommentPerson1]
    PRIMARY KEY CLUSTERED ([DislikeComments_Id], [PeopleDislike_Id] ASC);
GO

-- Creating primary key on [ReferQuestions_Id], [ReferPeople_Id] in table 'QuestionPerson1'
ALTER TABLE [dbo].[QuestionPerson1]
ADD CONSTRAINT [PK_QuestionPerson1]
    PRIMARY KEY CLUSTERED ([ReferQuestions_Id], [ReferPeople_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LikeComments_Id] in table 'CommentPerson'
ALTER TABLE [dbo].[CommentPerson]
ADD CONSTRAINT [FK_CommentPerson_Comment]
    FOREIGN KEY ([LikeComments_Id])
    REFERENCES [dbo].[Comments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PeopleLike_Id] in table 'CommentPerson'
ALTER TABLE [dbo].[CommentPerson]
ADD CONSTRAINT [FK_CommentPerson_Person]
    FOREIGN KEY ([PeopleLike_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentPerson_Person'
CREATE INDEX [IX_FK_CommentPerson_Person]
ON [dbo].[CommentPerson]
    ([PeopleLike_Id]);
GO

-- Creating foreign key on [DislikeComments_Id] in table 'CommentPerson1'
ALTER TABLE [dbo].[CommentPerson1]
ADD CONSTRAINT [FK_CommentPerson1_Comment]
    FOREIGN KEY ([DislikeComments_Id])
    REFERENCES [dbo].[Comments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PeopleDislike_Id] in table 'CommentPerson1'
ALTER TABLE [dbo].[CommentPerson1]
ADD CONSTRAINT [FK_CommentPerson1_Person]
    FOREIGN KEY ([PeopleDislike_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentPerson1_Person'
CREATE INDEX [IX_FK_CommentPerson1_Person]
ON [dbo].[CommentPerson1]
    ([PeopleDislike_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PersonComment]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonComment'
CREATE INDEX [IX_FK_PersonComment]
ON [dbo].[Comments]
    ([Person_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_QuestionPerson]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionPerson'
CREATE INDEX [IX_FK_QuestionPerson]
ON [dbo].[Questions]
    ([Person_Id]);
GO

-- Creating foreign key on [Question_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_QuestionComment]
    FOREIGN KEY ([Question_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionComment'
CREATE INDEX [IX_FK_QuestionComment]
ON [dbo].[Comments]
    ([Question_Id]);
GO

-- Creating foreign key on [ParentCommentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentComment]
    FOREIGN KEY ([ParentCommentId])
    REFERENCES [dbo].[Comments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentComment'
CREATE INDEX [IX_FK_CommentComment]
ON [dbo].[Comments]
    ([ParentCommentId]);
GO

-- Creating foreign key on [RoleId] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK_PersonPersonRole]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[PersonRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPersonRole'
CREATE INDEX [IX_FK_PersonPersonRole]
ON [dbo].[People]
    ([RoleId]);
GO

-- Creating foreign key on [ReferQuestions_Id] in table 'QuestionPerson1'
ALTER TABLE [dbo].[QuestionPerson1]
ADD CONSTRAINT [FK_QuestionPerson1_Question]
    FOREIGN KEY ([ReferQuestions_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReferPeople_Id] in table 'QuestionPerson1'
ALTER TABLE [dbo].[QuestionPerson1]
ADD CONSTRAINT [FK_QuestionPerson1_Person]
    FOREIGN KEY ([ReferPeople_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionPerson1_Person'
CREATE INDEX [IX_FK_QuestionPerson1_Person]
ON [dbo].[QuestionPerson1]
    ([ReferPeople_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_ChatPerson]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatPerson'
CREATE INDEX [IX_FK_ChatPerson]
ON [dbo].[Chats]
    ([Person_Id]);
GO

-- Creating foreign key on [FamilyId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_FamilyProduct]
    FOREIGN KEY ([FamilyId])
    REFERENCES [dbo].[Families]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FamilyProduct'
CREATE INDEX [IX_FK_FamilyProduct]
ON [dbo].[Products]
    ([FamilyId]);
GO

-- Creating foreign key on [Supportor_Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_QuestionPerson2]
    FOREIGN KEY ([Supportor_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionPerson2'
CREATE INDEX [IX_FK_QuestionPerson2]
ON [dbo].[Questions]
    ([Supportor_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_QuestionProduct]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionProduct'
CREATE INDEX [IX_FK_QuestionProduct]
ON [dbo].[Questions]
    ([Product_Id]);
GO

-- Creating foreign key on [Supportor_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductPerson]
    FOREIGN KEY ([Supportor_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPerson'
CREATE INDEX [IX_FK_ProductPerson]
ON [dbo].[Products]
    ([Supportor_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------