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
CREATE TABLE [Banner] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [Image] nvarchar(255) NULL,
    [Description] nvarchar(1000) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [Status] tinyint NOT NULL,
    CONSTRAINT [PK_Banner] PRIMARY KEY ([Id])
);

CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Status] tinyint NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [Image] nvarchar(255) NULL,
    [Price] decimal(18,2) NOT NULL,
    [SalePrice] decimal(18,2) NOT NULL,
    [Status] tinyint NOT NULL,
    [Description] nvarchar(1000) NULL,
    [CategoryId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_Product_CategoryId] ON [Product] ([CategoryId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260930050054_InitialCatalog', N'9.0.9');

COMMIT;
GO

