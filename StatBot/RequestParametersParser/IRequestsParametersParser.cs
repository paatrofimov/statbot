namespace StatBot.RequestParametersParser
{
    public interface IRequestsParametersParser<TRequestParams>
    {
        TRequestParams ParseParametersOrNull(string rawInput);
    }
}