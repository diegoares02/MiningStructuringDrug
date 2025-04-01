CREATE PROCEDURE sp_CreateUser
    @Username NVARCHAR(255),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(MAX),
    @PasswordSalt NVARCHAR(MAX),
    @Role NVARCHAR(100),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Users (Username, Email, PasswordHash, PasswordSalt, Role)
    VALUES (@Username, @Email, @PasswordHash, @PasswordSalt, @Role);

    SET @Id = SCOPE_IDENTITY();
END
GO