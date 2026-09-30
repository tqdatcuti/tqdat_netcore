IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [StdClass] (
    [Id] int NOT NULL IDENTITY,
    [ClassName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_StdClass] PRIMARY KEY ([Id])
);

CREATE TABLE [Subjects] (
    [Id] int NOT NULL IDENTITY,
    [SubjectName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id])
);

CREATE TABLE [Student] (
    [Id] int NOT NULL IDENTITY,
    [StudentName] nvarchar(100) NOT NULL,
    [StudentEmail] nvarchar(100) NOT NULL,
    [StudentPhone] nvarchar(50) NOT NULL,
    [StudentAddress] nvarchar(150) NOT NULL,
    [StudentAvatar] nvarchar(100) NOT NULL,
    [StudentBirthday] date NOT NULL,
    [ClassId] int NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Student_StdClass_ClassId] FOREIGN KEY ([ClassId]) REFERENCES [StdClass] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Marks] (
    [SubjectId] int NOT NULL,
    [StudentId] int NOT NULL,
    [Score] float NOT NULL,
    CONSTRAINT [PK_Marks] PRIMARY KEY ([SubjectId], [StudentId]),
    CONSTRAINT [FK_Marks_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Marks_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Marks_StudentId] ON [Marks] ([StudentId]);

CREATE INDEX [IX_Student_ClassId] ON [Student] ([ClassId]);

CREATE UNIQUE INDEX [IX_Student_StudentEmail] ON [Student] ([StudentEmail]);

CREATE UNIQUE INDEX [IX_Student_StudentPhone] ON [Student] ([StudentPhone]);

CREATE UNIQUE INDEX [IX_Subjects_SubjectName] ON [Subjects] ([SubjectName]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260930050104_InitialStudentManager', N'9.0.9');

COMMIT;
GO

