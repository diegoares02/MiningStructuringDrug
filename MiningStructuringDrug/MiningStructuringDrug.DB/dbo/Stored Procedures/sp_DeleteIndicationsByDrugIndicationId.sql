CREATE PROCEDURE sp_DeleteIndicationsByDrugIndicationId
    @DrugIndicationId INT
AS
BEGIN
    DELETE FROM Indications
    WHERE DrugIndicationId = @DrugIndicationId;
END
GO