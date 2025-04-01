CREATE TABLE CopayCards (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProgramName NVARCHAR(255) NOT NULL,
    ProgramType NVARCHAR(100),
    Funding_Evergreen NVARCHAR(50),
    Funding_CurrentFundingLevel NVARCHAR(255)
);