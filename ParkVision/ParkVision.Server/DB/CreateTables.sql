USE ParkVisionDB;
GO

-- Tabel: BilType
CREATE TABLE BilType (
    BilTypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeNavn NVARCHAR(20) NOT NULL
);

-- Tabel: BilModel
CREATE TABLE BilModel (
    ModelID INT PRIMARY KEY IDENTITY(1,1),
    Maerke NVARCHAR(50) NOT NULL,
    ModelNavn NVARCHAR(50) NOT NULL
);

-- Tabel: Bil
CREATE TABLE Bil (
    Nummerplade NVARCHAR(20) PRIMARY KEY,
    BilTypeID INT NOT NULL FOREIGN KEY REFERENCES BilType(BilTypeID),
    ModelID INT NOT NULL FOREIGN KEY REFERENCES BilModel(ModelID)
);

-- Tabel: Parkeringsplads
CREATE TABLE Parkeringsplads (
    ParkeringspladsID INT PRIMARY KEY IDENTITY(1,1),
    LedigePladser INT NOT NULL,
    Adresse NVARCHAR(100) UNIQUE NOT NULL -- Ændret til at være unik.
);

-- Tabel: Parkering
CREATE TABLE Parkering (
    ParkeringID INT PRIMARY KEY IDENTITY(1,1),
    Nummerplade NVARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Bil(Nummerplade),
    ParkeringspladsID INT NOT NULL FOREIGN KEY REFERENCES Parkeringsplads(ParkeringspladsID),
    IndkoerselTid DATETIME NOT NULL,
    UdkoerselTid DATETIME NOT NULL,
    ErPrivatFormaal BIT NOT NULL DEFAULT 1 -- Ændret til true/false.
);