-- lader til at virke
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(50) NOT NULL
);

CREATE TABLE Books (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    UploadDate DATETIME NOT NULL,
    FilePath NVARCHAR(200) NOT NULL
);



CREATE TABLE BookClubs (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Unik identifikator for bogklubben
    Name NVARCHAR(100) NOT NULL, -- Navn på bogklubben
    Description NVARCHAR(50) NOT NULL, -- Beskrivelse af bogklubben (maks. 50 tegn)
    OwnerId INT NOT NULL, -- Fremmednøgle til brugeren, der ejer/opretter bogklubben
    FOREIGN KEY (OwnerId) REFERENCES Users(Id) -- Relation til Users-tabellen
);


CREATE TABLE Messages (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BookClubId INT NOT NULL,
    UserId INT NOT NULL,
    MessageContent NVARCHAR(MAX) NOT NULL,
    Timestamp DATETIME NOT NULL,
    FOREIGN KEY (BookClubId) REFERENCES BookClubs(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE BookClubUsers (
    BookClubId INT NOT NULL,
    UserId INT NOT NULL,
    PRIMARY KEY (BookClubId, UserId),
    FOREIGN KEY (BookClubId) REFERENCES BookClubs(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);