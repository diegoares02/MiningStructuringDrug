CREATE PROCEDURE sp_GetUserByEmail
    @Email NVARCHAR(255)
AS
BEGIN
    SELECT Id,
           Username,
           Email,
           PasswordHash,
           PasswordSalt,
           Role
    FROM Users
    WHERE Email = @Email;
END
GO