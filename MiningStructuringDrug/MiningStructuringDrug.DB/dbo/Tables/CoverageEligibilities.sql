CREATE TABLE CoverageEligibilities (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CopayCardId INT NOT NULL,
    Eligibility NVARCHAR(255),
    FOREIGN KEY (CopayCardId) REFERENCES CopayCards(Id)
);