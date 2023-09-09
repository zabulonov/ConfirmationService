namespace ConfirmationService.Host;

public class UserTokens
{
    private List<UserToken> UserWithTokens { get; set; }

    public UserTokens(List<UserToken> tokens)
    {
        UserWithTokens = tokens;
    }

    public void AddUser(UserToken token)
    {
        UserWithTokens.Add(token);
    }
    
    public class UserToken
    {
        public Guid Token { get; set; }
        public string CompanyName { get; set; }
    }
}
