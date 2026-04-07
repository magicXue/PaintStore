using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PaintStore.Models;
using PaintStore.Models.Interfaces.Repositories;
using PaintStore.Services;
using Xunit;
namespace PaintStore.Tests;


public class UserServiceTests
{   
    private Mock<IUserRepository> _userRepositoryMock;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
    }

    [Fact]
    public async Task CreateUserAsync_WhenUserRepositorySucceeds_ReturnNewUser()
    {
        // Arrange
        var userService = new UserService(_userRepositoryMock.Object);
        var user = new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com"};
        _userRepositoryMock.Setup(repo => repo.AddUserToDbAsync(It.IsAny<User>())).ReturnsAsync(user);
        // Act
        var result = await userService.CreateUserAsync(user);
        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("John Doe");
        result.Email.Should().Be("johndoe@example.com");
        
    }

    [Fact]
    public async Task CreateUserAsync_WhenUserRepositoryThrowsException_ShouldThrowException()
    {
        // Arrange
        var userService = new UserService(_userRepositoryMock.Object);
        var user = new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };
        _userRepositoryMock.Setup(repo => repo.AddUserToDbAsync(It.IsAny<User>())).ThrowsAsync(new DbUpdateException("Add User failed"));

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(() => userService.CreateUserAsync(user));
    }
}

