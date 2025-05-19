USE ParkVisionDB;
GO

CREATE TABLE Bil (
    Nummerplade VARCHAR(7) PRIMARY KEY
);

CREATE TABLE Parkeringsplads (
    ParkeringspladsID INT PRIMARY KEY IDENTITY(1,1),
    LedigePladser INT NOT NULL,
    Adresse NVARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE Parkering (
    ParkeringID INT PRIMARY KEY IDENTITY(1,1),
    Nummerplade VARCHAR(7) NOT NULL FOREIGN KEY REFERENCES Bil(Nummerplade),
    ParkeringspladsID INT NOT NULL FOREIGN KEY REFERENCES Parkeringsplads(ParkeringspladsID) ON DELETE CASCADE,
    IndkoerselTid DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UdkoerselTid DATETIME NULL
);