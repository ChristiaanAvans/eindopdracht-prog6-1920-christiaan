CREATE TABLE [dbo].[Booking]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [BookingDate] DATETIME NOT NULL,
	[FirstName] VARCHAR(MAX) NOT NULL, 
    [LastName] VARCHAR(MAX) NOT NULL, 
    [Insertion] VARCHAR(MAX) NULL, 
    [Address] VARCHAR(MAX) NOT NULL, 
    [Mail] VARCHAR(MAX) NULL, 
    [PhoneNumber] VARCHAR(MAX) NULL
)
