namespace StatBot.UserIO
{
    public static class UserMessagesBuilder
    {
        public static string InputNotExpected(string expected)
        {
            return $"Input is not one of the expected: {expected}";
        }

        public static string PrintNoUserOrGroupWithScreenName(string screenName)
        {
            return $"No user or group with screen name: {screenName}";
        }

        public static string PrintPostingStatsMessageToGroupWithId(long groupId)
        {
            return $"Posting stats message to group with id: {groupId}";
        }
    }
}