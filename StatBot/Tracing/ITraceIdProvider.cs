namespace StatBot.Tracing
{
    public interface ITraceIdProvider
    {
        string GetNext();
    }
}