using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Handlers;
using MiningStructuringDrug.Core.Application.Users.Interfaces;
using MiningStructuringDrug.Core.Application.Users.Queries;
using MiningStructuringDrug.Core.Domain.Entities;
using MiningStructuringDrug.Core.Domain.Interfaces;
using Moq;

namespace MiningStructuringDrug.Test.Application.DrugIndications.Handlers
{
    public class UserHandlersTests
    {
        [Fact]
        public async Task CreateUserCommandHandler_ValidCommand_ReturnsUserDto()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockAuthenticationRepo = new Mock<IAuthenticationService>();
            var handler = new CreateUserCommandHandler(mockRepo.Object, mockAuthenticationRepo.Object);
            var command = new CreateUserCommand
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "password",
                Role = "User"
            };

            mockAuthenticationRepo.Setup(repo => repo.RegisterAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync((User user, string password) => user);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
            Assert.Equal("testuser", result.Username);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task AuthenticateUserCommandHandler_ValidCredentials_ReturnsAuthResponseDto()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockHasher = new Mock<IPasswordHasher<User>>();
            var mockAuthenticationRepo = new Mock<IAuthenticationService>();
            var handler = new AuthenticateUserCommandHandler(mockRepo.Object, mockAuthenticationRepo.Object);
            var command = new AuthenticateUserCommand
            {
                Username = "testuser",
                Password = "password"
            };
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                PasswordHash = "hashedpassword",
                PasswordSalt = "salt",
                Role = "User"
            };

            mockRepo.Setup(repo => repo.GetByUsernameAsync("testuser")).ReturnsAsync(user);
            mockHasher.Setup(hasher => hasher.VerifyHashedPassword(user, "hashedpassword", "password")).Returns(PasswordVerificationResult.Success);
            mockAuthenticationRepo.Setup(repo => repo.GenerateJwtToken(user)).Returns("testtoken");
            mockAuthenticationRepo.Setup(repo => repo.AuthenticateAsync("testuser", "password")).ReturnsAsync(user);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.User);
            Assert.NotNull(result.Token);
            Assert.Equal(1, result.User.Id);
        }

        [Fact]
        public async Task AuthenticateUserCommandHandler_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockHasher = new Mock<IPasswordHasher<User>>();
            var mockAuthenticationRepo = new Mock<IAuthenticationService>();
            var handler = new AuthenticateUserCommandHandler(mockRepo.Object, mockAuthenticationRepo.Object);
            var command = new AuthenticateUserCommand
            {
                Username = "testuser",
                Password = "wrongpassword"
            };
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                PasswordHash = "hashedpassword",
                PasswordSalt = "salt",
                Role = "User"
            };

            mockRepo.Setup(repo => repo.GetByUsernameAsync("testuser")).ReturnsAsync(user);
            mockHasher.Setup(hasher => hasher.VerifyHashedPassword(user, "hashedpassword", "wrongpassword")).Returns(PasswordVerificationResult.Failed);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserQueryHandler_ValidId_ReturnsUserDto()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var handler = new GetUserQueryHandler(mockRepo.Object);
            var query = new GetUserQuery { Id = 1 };
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Email = "test@example.com",
                Role = "User"
            };

            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await handler.Handle(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("testuser", result.Username);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task GetUserQueryHandler_InvalidId_ReturnsNull()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var handler = new GetUserQueryHandler(mockRepo.Object);
            var query = new GetUserQuery { Id = 99 }; // Invalid ID

            mockRepo.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((User)null);

            // Act
            var result = await handler.Handle(query);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUserCommandHandler_ValidCommand_CallsRepositoryDelete()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var handler = new DeleteUserCommandHandler(mockRepo.Object);
            var command = new DeleteUserCommand { Id = 1 };

            // Act
            await handler.Handle(command);

            // Assert
            mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task UpdateUserRoleCommandHandler_ValidCommand_CallsRepositoryUpdate()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var handler = new UpdateUserRoleCommandHandler(mockRepo.Object);
            var command = new UpdateUserRoleCommand { Id = 1, Role = "Admin" };

            // Act
            await handler.Handle(command);

            // Assert
            mockRepo.Verify(repo => repo.UpdateAsync(It.Is<User>(u => u.Id == 1 && u.Role == "Admin")), Times.Once);
        }
    }
}
