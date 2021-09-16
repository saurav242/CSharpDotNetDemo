CREATE TABLE Employee
(
    Id INT NOT NULL IDENTITY,
    Email VARCHAR(50),
    UserName VARCHAR(100) ,
    DeptId INT NOT NULL,
    PRIMARY KEY (Id, Email),
    UNIQUE(UserName, Email)
)

CREATE TABLE Department
{
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    Name VARCHAR(100),


}