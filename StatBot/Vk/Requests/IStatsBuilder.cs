namespace StatBot.Vk.Requests
{
    public interface IStatsBuilder<TStatsBuilderData>
    {
        string Build(TStatsBuilderData data);
    }
}