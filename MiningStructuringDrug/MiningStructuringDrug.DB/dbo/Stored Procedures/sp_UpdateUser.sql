CREATE PROCEDURE sp_UpdateUser
    @Id INT,
    @Username NVARCHAR(255),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(MAX),
    @PasswordSalt NVARCHAR(MAX),
    @Role NVARCHAR(100)
AS
BEGIN
    UPDATE Users
    SET Username = @Username,
        Email = @Email,
        PasswordHash = @PasswordHash,
        PasswordSalt = @PasswordSalt,
        Role = @Role
    WHERE Id = @Id;
END
GO