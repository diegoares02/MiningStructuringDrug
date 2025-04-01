CREATE PROCEDURE sp_GetDrugIndicationById
    @Id INT
AS
BEGIN
    SELECT Id, DrugName
    FROM DrugIndications
    WHERE Id = @Id;
END
GO
