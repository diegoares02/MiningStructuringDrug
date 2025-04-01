CREATE TABLE Details (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CopayCardId INT NOT NULL,
    Eligibility NVARCHAR(MAX),
    Program NVARCHAR(MAX),
    Renewal NVARCHAR(MAX),
    Income NVARCHAR(MAX),
    FOREIGN KEY (CopayCardId) REFERENCES CopayCards(Id)
);