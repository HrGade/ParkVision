USE ParkVisionDB;
GO

INSERT INTO [dbo].[Bil] (
[Nummerplade]
)
VALUES
('TEST123'),
('TEST857'),
('TEST374');

INSERT INTO [dbo].[Parkeringsplads] (
[LedigePladser],
[Adresse]
)
VALUES
(50, 'Adresse1'),
(100, 'Adresse2');

--INSERT INTO [dbo].[Parkering] (
--[Nummerplade],
--[ParkeringspladsID],
--[IndkoerselTid],
--[UdkoerselTid]
--)
--VALUES
--('TEST123', 1, ,),
--('TEST857', 2, ,);