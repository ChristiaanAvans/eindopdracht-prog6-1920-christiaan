﻿CREATE TABLE [dbo].[Accessoire]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(MAX) NOT NULL, 
    [Price] DECIMAL(20, 2) NOT NULL, 
    [ImageUrl] VARCHAR(MAX) NULL, 
    [Animal] INT NOT NULL,

	CONSTRAINT PK_Accessoire_Id PRIMARY KEY ([Id]),
	CONSTRAINT FK_Animal FOREIGN KEY ([Animal]) REFERENCES Animal ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
)
