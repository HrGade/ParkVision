-- Skal kun køres på test-database, ikke kundens rigtige database! -- 

USE ParkVisionDB;
GO

--TRUNCATE TABLE [dbo].[BilType]

INSERT INTO [dbo].[BilType] (
[TypeNavn]
)
VALUES
('SUV'),
('Sedan'),
('Minivan');

--TRUNCATE TABLE [dbo].[BilModel]

INSERT INTO [dbo].[BilModel] (
[Maerke],
[ModelNavn]
)
VALUES
('Mærke1', 'ModelNavn1'),
('Mærke2', 'ModelNavn2'),
('Mærke3', 'ModelNavn3');

--TRUNCATE TABLE [dbo].[Bil]

INSERT INTO [dbo].[Bil] (
[Nummerplade],
[BilTypeID],
[ModelID]
)
VALUES
('TEST1235', 1, 1),
('TEST8572', 2, 2),
('TEST3744', 3, 3);

-- Mangler Parkering og Pakeringsplads.