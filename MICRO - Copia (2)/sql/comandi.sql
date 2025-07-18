CREATE DATABASE Guidici
use Giudici;
CREATE TABLE Persone (
    Id INT PRIMARY KEY IDENTITY(1,1),      -- ID univoco per la persona, incremento automatico
    Nome NVARCHAR(100) NOT NULL,            -- Nome della persona
    Cognome NVARCHAR(100) NOT NULL,         -- Cognome della persona
    CodiceFiscale NVARCHAR(16) NOT NULL     -- Codice Fiscale della persona (lunghezza tipica)
);
CREATE TABLE TransactionalOutboxList (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Tabella NVARCHAR(50) NOT NULL DEFAULT 'Votazioni',
    Messaggio NVARCHAR(MAX) NOT NULL,
);

CREATE TABLE VotazioniKafka (
    Id INT PRIMARY KEY IDENTITY(1,1),     -- Identificativo unico per la votazione
    IdGiudice INT NOT NULL,                         -- ID della persona che è giudice e dà il voto
    IdCane INT NOT NULL,                            -- ID della persona che è cane e riceve il voto
    Voto INT NOT NULL,                              -- Voto assegnato (può essere un numero intero)
    FOREIGN KEY (IdGiudice)  REFERENCES Persone(Id)
  );


CREATE DATABASE Registrazioni
USE Registrazioni
CREATE TABLE Cani (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Razza NVARCHAR(100) NOT NULL,
);

CREATE TABLE TransactionalOutboxList (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Tabella NVARCHAR(50) NOT NULL DEFAULT 'Votazioni',
    Messaggio NVARCHAR(MAX) NOT NULL,
    
);

CREATE TABLE Votazioni (
    Id INT PRIMARY KEY IDENTITY(1,1),     -- Identificativo unico per la votazione
    IdGiudice INT NOT NULL,                         -- ID della persona che è giudice e dà il voto
    IdCane INT NOT NULL,                            -- ID della persona che è cane e riceve il voto
    Voto INT NOT NULL,                              -- Voto assegnato (può essere un numero intero)
    FOREIGN KEY (IdCane)  REFERENCES Cani(Id)
  );

CREATE DATABASE Proprietari
USE Proprietari
CREATE TABLE Soggetti (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Cognome NVARCHAR(100) NOT NULL,
    CF NVARCHAR(16) NOT NULL,
);

CREATE TABLE TransactionalOutboxList (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Tabella NVARCHAR(50) NOT NULL DEFAULT 'Iscrizioni',
    Messaggio NVARCHAR(MAX) NOT NULL,
    
);

Create table CaneProprietari(
    id int PRIMARY KEY IDENTITY(1,1),
    IdCane INT NOT NULL,
    IdProprietario INT NOT NULL,
    FOREIGN KEY (IdProprietario) REFERENCES Soggetti(Id)
       
)




