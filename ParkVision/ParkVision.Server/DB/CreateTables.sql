USE ParkVisionDB;
GO

-- Tabel: Bil
CREATE TABLE Bil (
    Nummerplade NVARCHAR(20) PRIMARY KEY,
    EjerNavn NVARCHAR(50) NOT NULL,
    BilTypeID INT NOT NULL FOREIGN KEY REFERENCES BilType(BilTypeID),
    ModelID INT NOT NULL FOREIGN KEY REFERENCES BilModel(ModelID),
    TypeNavn NVARCHAR(20) NOT NULL,
    Maerke NVARCHAR(50) NOT NULL,
    ModelNavn NVARCHAR(50) NOT NULL,
    ErPrivatFormaal BIT NOT NULL DEFAULT 1
);

-- Tabel: Parkeringsplads
CREATE TABLE Parkeringsplads (
    ParkeringspladsID INT PRIMARY KEY IDENTITY(1,1),
    LedigePladser INT NOT NULL,
    Adresse NVARCHAR(100) UNIQUE NOT NULL
);

-- Tabel: Parkering
CREATE TABLE Parkering (
    ParkeringID INT PRIMARY KEY IDENTITY(1,1),
    Nummerplade NVARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Bil(Nummerplade),
    ParkeringspladsID INT NOT NULL FOREIGN KEY REFERENCES Parkeringsplads(ParkeringspladsID),
    IndkoerselTid DATETIME NOT NULL,
    UdkoerselTid DATETIME NULL
);