// using ConfirmationService.BusinessLogic.Services;
//
// namespace ConfirmationService.Tests;
//
// public class UserServiceTests
// {
//     private readonly UserService _service;
//
//     public const string ValidName = "testName";
//
//     // public UserServiceTests(UserService service)
//     // {
//     //     _service = service;
//     // }
//
//     
//     // что это получается, мне нужено сделать мок, Userservice, ConfirmServiceContext, mailSendService что затестить 1 функцию?
//     [SetUp]
//     public void Setup(UserService service)
//     {
//         _service = new UserService();
//     }
//     
//     [TestCase(ValidName)]
//     [Test]
//     public void TestRegisterNewUser_ValidInput(string conpanyName)
//     {
//         
//         Assert.Pass();
//     }
// }