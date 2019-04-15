namespace StatBot.UserIO
{
    /// <summary>
    /// calling <code>PostingStatsMessageToGroupWithId(123)</code> will print <code>Posting Stats Message To Group With Id. groupId: {groupId}</code>
    /// </summary>
    public interface IUserMessagePrinter
    {
        IMessagePrinter UnderlyingPrinter { get; }

        void PrintNextRequestParameters();
        void DoYouWantToWaitPendingRequests();
        void YesOrNo();
        void InputNotExpected(string expected);
        void RequestExecutionStarted();
        void RequestExecutionEnded();
        void NoUserOrGroup(string screenName);
        void PostingStatsMessageToGroupWithId(long groupId);
    }
}