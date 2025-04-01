CREATE PROCEDURE sp_CreateDrugIndication
    @DrugName NVARCHAR(255),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO DrugIndications (DrugName)
    VALUES (@DrugName);

    SET @Id = SCOPE_IDENTITY();  --  Get the newly inserted ID
END
GO