namespace StatBot.UserIO
{
    public interface IUserConsentProvider
    {
        bool UserConsents(string rawInput);
        bool ConsentFormatIsValid(string rawInput);
    }
}