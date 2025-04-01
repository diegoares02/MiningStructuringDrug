CREATE PROCEDURE sp_GetUserByUsername
    @Username NVARCHAR(255)
AS
BEGIN
    SELECT Id,
           Username,
           Email,
           PasswordHash,
           PasswordSalt,
           Role
    FROM Users
    WHERE Username = @Username;
END
GO