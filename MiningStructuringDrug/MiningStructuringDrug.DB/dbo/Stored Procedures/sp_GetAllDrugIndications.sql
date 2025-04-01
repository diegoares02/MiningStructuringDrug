CREATE PROCEDURE sp_GetAllDrugIndications
AS
BEGIN
    SELECT Id, DrugName
    FROM DrugIndications;
END
GO