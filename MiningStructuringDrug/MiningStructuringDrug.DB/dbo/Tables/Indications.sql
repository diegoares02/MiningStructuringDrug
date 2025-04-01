CREATE TABLE Indications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DrugIndicationId INT NOT NULL,
    Indication NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (DrugIndicationId) REFERENCES DrugIndications(Id)
);