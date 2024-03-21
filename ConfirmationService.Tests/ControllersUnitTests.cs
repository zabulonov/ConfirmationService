using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.BusinessLogic.Services.Interfaces;
using ConfirmationService.Core.Entity;
using ConfirmationService.Host.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ConfirmationService.Tests;

public class ControllersUnitTests
{
    [Fact]
    public void RegisterTest()
    {
        var MockUserService = new Mock<IUserService>();
        MockUserService.Setup(service => service.RegisterNewUser("Unit test Company")).Returns(RegisterMock());
        
        UserController userController = new UserController(MockUserService.Object, new HttpContextAccessor());

        var result = userController.NewUser("Unit test Company").Result;

        Assert.True(result is Guid and != null);
    }
    
    // [Fact]
    // public void DeleteTest()
    // {
    //     
    // }
    
    
    private Task<Guid> RegisterMock()
    {
        return Task.FromResult(new Guid());
    }
    //
    // private User[] _users = new []{} 
    //
    // private Task<bool> DeleteUser(Guid token)
    // {   
    //     try
    //     {
    //         confirmServiceContext.DeleteUserByToken(token);
    //         return true;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return false;
    //     }
    //
    // }
}

