CREATE PROCEDURE sp_CreateICD10Code
    @DrugIndicationId INT,
    @Code NVARCHAR(20)
AS
BEGIN
    INSERT INTO ICD10Codes (DrugIndicationId, ICD10Code)
    VALUES (@DrugIndicationId, @Code);
END
GO