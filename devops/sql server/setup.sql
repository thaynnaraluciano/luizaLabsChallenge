CREATE DATABASE LuizaLabsChallenge;
GO

USE LuizaLabsChallenge;
GO

CREATE TABLE Users ( 
    id varchar(216) PRIMARY KEY,
    username varchar(216) NOT NULL,
    email varchar(216) NOT NULL,
    password varchar(216) NOT NULL,
    isEmailConfirmed char(1) NOT NULL default(0))
GO
