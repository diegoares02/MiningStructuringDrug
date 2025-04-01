CREATE PROCEDURE sp_CreateIndication
    @DrugIndicationId INT,
    @IndicationText NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Indications (DrugIndicationId, Indication)
    VALUES (@DrugIndicationId, @IndicationText);
END
GO