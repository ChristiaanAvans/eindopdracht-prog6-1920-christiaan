CREATE TABLE [dbo].[BookedAccessoire]
(
	[Accessoire] INT NOT NULL, 
    [Booking] INT NOT NULL,

	CONSTRAINT PK_BookedAccessoire_Ids PRIMARY KEY ([Accessoire], [Booking]),
	CONSTRAINT FK_Accessoire_Id FOREIGN KEY ([Accessoire]) REFERENCES Accessoire ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_AccessoireBooking_Id FOREIGN KEY ([Booking]) REFERENCES Booking ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
)
