CREATE PROCEDURE sp_UpdateDrugIndication
    @Id INT,
    @DrugName NVARCHAR(255)
AS
BEGIN
    UPDATE DrugIndications
    SET DrugName = @DrugName
    WHERE Id = @Id;
END
GO