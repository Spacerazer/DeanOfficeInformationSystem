-- Создание базы данных
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DeanOfficeDB')
BEGIN
    CREATE DATABASE DeanOfficeDB;
END
GO

USE DeanOfficeDB;
GO

-- Создание таблицы студентов
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Student](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [MiddleName] [nvarchar](50) NULL,
        [Group] [nvarchar](20) NOT NULL,
        [Course] [int] NOT NULL,
        [Speciality] [nvarchar](100) NOT NULL,
        CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END
GO

-- Создание таблицы сотрудников
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Employee](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [MiddleName] [nvarchar](50) NULL,
        [Position] [nvarchar](100) NOT NULL,
        [Department] [nvarchar](100) NOT NULL,
        [Phone] [nvarchar](20) NULL,
        [Email] [nvarchar](100) NULL,
        CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END
GO

-- Создание таблицы учебных групп
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudyGroup]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[StudyGroup](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [GroupName] [nvarchar](20) NOT NULL,
        [Course] [int] NOT NULL,
        [Speciality] [nvarchar](100) NOT NULL,
        [FormationYear] [int] NOT NULL,
        [HeadmanId] [int] NULL,
        CONSTRAINT [PK_StudyGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UQ_StudyGroup_GroupName] UNIQUE ([GroupName]),
        CONSTRAINT [FK_StudyGroup_Student] FOREIGN KEY ([HeadmanId]) REFERENCES [dbo].[Student] ([Id]),
        CONSTRAINT [CK_StudyGroup_Course] CHECK ([Course] BETWEEN 1 AND 6),
        CONSTRAINT [CK_StudyGroup_FormationYear] CHECK ([FormationYear] BETWEEN 1900 AND YEAR(GETDATE()))
    );
END
GO 