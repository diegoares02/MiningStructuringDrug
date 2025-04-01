CREATE PROCEDURE sp_GetUserById
    @Id INT
AS
BEGIN
    SELECT Id,
           Username,
           Email,
           PasswordHash,
           PasswordSalt,
           Role
    FROM Users
    WHERE Id = @Id;
END
GO