CREATE PROCEDURE sp_DeleteDrugIndication
    @Id INT
AS
BEGIN
    DELETE FROM DrugIndications
    WHERE Id = @Id;
END
GO