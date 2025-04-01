using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Dtos;
using MiningStructuringDrug.Infrastructure.Services.User;
using Moq;

namespace MiningStructuringDrug.Test.Infrastructure.Services.User
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserAsync_ValidCommand_ReturnsUserDto()
        {
            // Arrange
            var mockDispatcher = new Mock<IMessageDispatcher>();
            var createUserCommand = new CreateUserCommand { Username = "testuser", Email = "test@example.com" };
            var expectedUserDto = new UserDto { Id = 1, Username = "testuser", Email = "test@example.com" };

            mockDispatcher.Setup(d => d.DispatchCommand<CreateUserCommand, UserDto>(createUserCommand))
                .ReturnsAsync(expectedUserDto);

            var userService = new UserService(mockDispatcher.Object);

            // Act
            var result = await userService.CreateUserAsync(createUserCommand);

            // Assert
            Assert.Equal(expectedUserDto, result);
        }

        [Fact]
        public async Task AuthenticateUserAsync_ValidCommand_ReturnsAuthResponseDto()
        {
            // Arrange
            var mockDispatcher = new Mock<IMessageDispatcher>();
            var authenticateUserCommand = new AuthenticateUserCommand { Username = "testuser", Password = "password" };
            var expectedAuthResponseDto = new AuthResponseDto { Token = "testtoken", User = new UserDto() };

            mockDispatcher.Setup(d => d.DispatchCommand<AuthenticateUserCommand, AuthResponseDto?>(authenticateUserCommand))
                .ReturnsAsync(expectedAuthResponseDto);

            var userService = new UserService(mockDispatcher.Object);

            // Act
            var result = await userService.AuthenticateUserAsync(authenticateUserCommand);

            // Assert
            Assert.Equal(expectedAuthResponseDto, result);
        }

        [Fact]
        public async Task AuthenticateUserAsync_InvalidCommand_ReturnsNull()
        {
            // Arrange
            var mockDispatcher = new Mock<IMessageDispatcher>();
            var authenticateUserCommand = new AuthenticateUserCommand { Username = "testuser", Password = "password" };

            mockDispatcher.Setup(d => d.DispatchCommand<AuthenticateUserCommand, AuthResponseDto?>(authenticateUserCommand))
                .ReturnsAsync((AuthResponseDto?)null);

            var userService = new UserService(mockDispatcher.Object);

            // Act
            var result = await userService.AuthenticateUserAsync(authenticateUserCommand);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ValidUserId_CallsDispatcher()
        {
            // Arrange
            var mockDispatcher = new Mock<IMessageDispatcher>();
            var userService = new UserService(mockDispatcher.Object);
            int userId = 1;

            // Act
            await userService.DeleteUserAsync(userId);

            // Assert
            mockDispatcher.Verify(d => d.Dispatch<DeleteUserCommand>(It.Is<DeleteUserCommand>(cmd => cmd.Id == userId)), Times.Once);
        }

        [Fact]
        public async Task UpdateUserRoleAsync_ValidCommand_CallsDispatcher()
        {
            // Arrange
            var mockDispatcher = new Mock<IMessageDispatcher>();
            var userService = new UserService(mockDispatcher.Object);
            var updateUserRoleCommand = new UpdateUserRoleCommand { Id = 1, Role = "Admin" };

            // Act
            await userService.UpdateUserRoleAsync(updateUserRoleCommand);

            // Assert
            mockDispatcher.Verify(d => d.Dispatch<UpdateUserRoleCommand>(updateUserRoleCommand), Times.Once);
        }
    }
}
