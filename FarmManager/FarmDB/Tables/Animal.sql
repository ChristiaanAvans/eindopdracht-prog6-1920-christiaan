﻿CREATE TABLE [dbo].[Animal]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(MAX) NOT NULL, 
    [Price] DECIMAL(20, 2) NOT NULL, 
    [ImageUrl] VARCHAR(MAX) NULL, 
    [TypeName] VARCHAR(50) NOT NULL,

	CONSTRAINT PK_Animal_Id PRIMARY KEY ([Id]),
	CONSTRAINT FK_TypeName FOREIGN KEY ([TypeName]) REFERENCES [Type] ([Name]) ON DELETE CASCADE ON UPDATE CASCADE
)