CREATE TABLE Benefits (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CopayCardId INT NOT NULL,
    Name NVARCHAR(255),
    Value NVARCHAR(255),
    FOREIGN KEY (CopayCardId) REFERENCES CopayCards(Id)
);