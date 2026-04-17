CREATE DATABASE Student;
GO

USE Student;
GO

CREATE TABLE Registration
(
    regNo INT PRIMARY KEY,
    firstname VARCHAR(50),
    lastname VARCHAR(50),
    dateOfBirth DATETIME,
    gender VARCHAR(50),
    address VARCHAR(50),
    email VARCHAR(50),
    mobilePhone INT,
    homePhone INT,
    parentName VARCHAR(50),
    nic VARCHAR(50),
    contactNo INT
);
GO