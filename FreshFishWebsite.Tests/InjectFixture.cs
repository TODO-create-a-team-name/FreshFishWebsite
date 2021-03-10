using FreshFishWebsite.Models;
using FreshFishWebsite.Tests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

public class InjectFixture : IDisposable
{
    public readonly UserManager<User> UserManager;
    public readonly SignInManager<User> SignInManager;
    //public readonly IAccountService AccountService;
    public readonly FreshFishDbContext DbContext;
    //public readonly IGenerator Generator;

    public InjectFixture()
    {
        var options = new DbContextOptionsBuilder<FreshFishDbContext>()
            .UseInMemoryDatabase(databaseName: "FakeDatabase")
            .Options;

        DbContext = new FreshFishDbContext(options);

        var users = new List<User>
            {
                new User
                {
                    UserName = "Test",
                    Id = Guid.NewGuid().ToString(),
                    Email = "test@test.it"
                }
            }.AsQueryable();

        var fakeUserManager = new Mock<FakeUserManager>();

        fakeUserManager.Setup(x => x.Users)
            .Returns(users);

        fakeUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>()))
            .ReturnsAsync(IdentityResult.Success);
        fakeUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        fakeUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(IdentityResult.Success);
        fakeUserManager.Setup(x =>
                x.ChangeEmailAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var signInManager = new Mock<FakeSignInManager>();
        signInManager.Setup(
                x => x.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(),
                    It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);

        UserManager = fakeUserManager.Object;
        SignInManager = signInManager.Object;
        //AccountService = new AccountService(UserManager);
        //Generator = new Generator();
    }

    public void Dispose()
    {
        UserManager?.Dispose();
        DbContext?.Dispose();
    }
}