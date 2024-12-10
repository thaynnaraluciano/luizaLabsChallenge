CREATE DATABASE LuizaLabsChallenge;
GO

USE LuizaLabsChallenge;
GO

CREATE TABLE Users ( 
    id UNIQUEIDENTIFIER PRIMARY KEY,
    username varchar(216) UNIQUE NOT NULL,
    email varchar(216) UNIQUE NOT NULL,
    password varchar(216) NOT NULL,
    confirmedAt DATETIME default(NULL))
GO
