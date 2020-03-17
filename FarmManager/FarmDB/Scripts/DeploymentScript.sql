/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r ..\Tables\Type.sql
:r ..\Tables\Animal.sql
:r ..\Tables\Accessoire.sql
:r ..\Tables\Booking.sql
:r ..\Tables\BookedAnimal.sql
:r ..\Tables\BookedAccessoire.sql

:r ..\Scripts\Seed\FillTypes.sql
:r ..\Scripts\Seed\AnimalFill.sql
:r ..\Scripts\Seed\AccessoireFill.sql