CREATE PROCEDURE sp_DeleteICD10CodesByDrugIndicationId
    @DrugIndicationId INT
AS
BEGIN
    DELETE FROM ICD10Codes
    WHERE DrugIndicationId = @DrugIndicationId;
END
GO