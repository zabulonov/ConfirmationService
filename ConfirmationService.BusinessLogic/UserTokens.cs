namespace ConfirmationService.BusinessLogic;

public static class UserTokens
{
    private static List<UserToken>? UserWithTokens { get; set; }

    public static List<UserToken>? GetTokens()
    {
        return UserWithTokens;
    }

    public static void AddUser(UserToken token)
    {
        UserWithTokens!.Add(token);
    }

    public class UserToken
    {
        public Guid Token { get; set; }
        public string CompanyName { get; set; } = null!;
    }

    public static void Initialize(List<UserToken>? tokens)
    {
        if (UserWithTokens == null)
        {
            UserWithTokens = tokens;
        }
        else
        {
            throw new ArgumentException($"{nameof(UserWithTokens)} is already initialized");
        }
    }
}