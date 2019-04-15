namespace StatBot.Tracing
{
    public class TraceIdPlaneNumberProvider : ITraceIdProvider
    {
        private int counter;

        public string GetNext()
        {
            return counter++.ToString();
        }
    }
}