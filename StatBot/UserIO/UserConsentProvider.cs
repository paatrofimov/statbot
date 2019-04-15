using System;

namespace StatBot.UserIO
{
    public class UserConsentProvider : IUserConsentProvider
    {
        public bool UserConsents(string rawInput)
        {
            return rawInput.Equals(UserMessagesConstants.Yes, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool ConsentFormatIsValid(string rawInput)
        {
            return UserConsents(rawInput) || UserDoesntConsent(rawInput);
        }

        private bool UserDoesntConsent(string rawInput)
        {
            return rawInput.Equals(UserMessagesConstants.No, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}