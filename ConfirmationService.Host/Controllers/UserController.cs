using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.BusinessLogic.Services.Interfaces;
using ConfirmationService.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("User")]
[Authorize]
public class UserController(
    IUserService userService,
    IHttpContextAccessor httpContextAccessor)
{
    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <remarks>
    /// Registers a new user (company) to use the service, returns a token
    /// 
    /// No authorization
    /// </remarks>
    /// <param name="companyName">String Name of your company</param>
    /// <response code="200">OK Registration was successful, returned the token in the response</response>
    [HttpPut("Register")]
    [AllowAnonymous]
    public async Task<Guid> NewUser(string companyName)
    {
        return await userService.RegisterNewUser(companyName);
    }
    
    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <remarks>
    /// Removes the user from the database, his token is no longer valid, he cannot send emails to his clients
    ///
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <response code="200">OK user delete successfully</response>
    /// <response code="401">Authorization error, check the token in the header</response>
    [HttpDelete("DeleteMyself")]
    public async Task Delete()
    {
        await userService.DeleteUser(GetHeaderToken());
    }
    /// <summary>
    /// Sends an email to your client
    /// </summary>
    /// <remarks>
    /// Send an email using MailKit via SMTP.
    /// 
    /// Sends an email to your client with a link to confirm mail, the link comes to the mail, the client clicks on it and the mail is considered confirmed
    /// 
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <param name="userClient">All parameters are required</param>
    /// <response code="200">OK email sent successfully</response>
    /// <response code="401">Authorization error, check the token in the header</response>
    /// <response code="500">Error connecting to email client. Check that the connection data is correct in appsettings -> MailConnect</response>
    [HttpPost("CreateAndSendToClient")]
    public async Task CreateAndSendToClient([FromBody] UserClientModel userClient)
    { 
         await userService.SendConfirmationEmail(userClient, GetHeaderToken());
    }
    
    /// <summary>
    /// Return info about all your clients
    /// </summary>
    /// <remarks>
    /// Returns in the form of a json information about all your clients and the confirmation status of all their mails
    /// 
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <response code="200">OK return info</response>
    /// <response code="401">Authorization error, check the token in the header</response>
    [HttpGet("GetClients")]
    public async Task<List<ClientModel>> GetClients()
    {
        return await userService.GetUserClients(GetHeaderToken());
    }
    
    /// <summary>
    /// Return info about one your client
    /// </summary>
    /// <remarks>
    /// Returns in the form of a json all information about one of your clients and the confirmation status of all their mails.
    /// 
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <response code="200">OK return info</response>
    /// <response code="201">No information for this email</response>
    [HttpGet("GetOneClient")]
    public async Task<ClientOfUser?> GetOneClient(String clientMail)
    {
        return await userService.GetClientByMail(GetHeaderToken(), clientMail);
    }

    /// <summary>
    /// Sends confirmation email again
    /// </summary>
    /// <remarks>
    /// Sends an email to your client with a link to confirm mail, the link comes to the mail, the client clicks on it and the mail is considered confirmed.
    /// 
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <response code="200">OK email sent successfully</response>
    /// <response code="401">Authorization error, check the token in the header</response>
    /// <response code="500">Error connecting to email client. Check that the connection data is correct in appsettings -> MailConnect</response>
    [HttpPost("ResendMail")]
    public async Task ResendMailToClient(String Mail)
    {
        await userService.ResendingConfirmation(Mail, GetHeaderToken());
    }
    
    /// <summary>
    /// Return true/false email confirm status
    /// </summary>
    /// <remarks>
    /// Return confirmation status(true/false) about one your client
    /// 
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <response code="200">OK return info</response>
    /// <response code="201">No information for this email</response>
    [HttpGet("GetOneClientStatus")]
    public async Task<bool> GetOneClientStatus(String clientMail)
    {
        return await userService.GetClientByMailStatus(GetHeaderToken(), clientMail);
    }
    /// <summary>
    /// Sends email with confirm status of your clients.
    /// </summary>
    /// <remarks>
    /// Sends a email with a table containing information about each of your clients (name, email, confirmation status).
    /// 
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <response code="200">OK return info</response>
    /// /// <response code="401">Authorization error, check the token in the header</response>
    /// <response code="500">Error connecting to email client. Check that the connection data is correct in appsettings -> MailConnect</response>
    [HttpGet("GetClientsStatusByMail")]
    public async Task GetClientsStatusByMail(String userMail)
    {
        await userService.GetClientsStatusByMail(GetHeaderToken(),userMail);
    }
    private Guid GetHeaderToken()
    {
        var headers = httpContextAccessor.HttpContext?.Request.Headers;
        var myTokenValue = headers!["MyToken"];
        
        return Guid.Parse(myTokenValue!);
    }
}