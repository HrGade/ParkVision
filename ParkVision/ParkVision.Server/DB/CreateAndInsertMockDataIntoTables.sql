-- Skal kun køres på test-database, ikke kundens rigtige database! -- 

USE ParkVisionDB;
GO

--TRUNCATE TABLE [dbo].[Bil]

INSERT INTO [dbo].[Bil] (
[Nummerplade]
)
VALUES
('TEST1235'),
('TEST8572'),
('TEST3744');

-- Mangler Parkering og Pakeringsplads.