namespace StatBot.VkApiClient.Models.Internal
{
    public class InternalVkApiObject
    {
        public long? Id { get; set; }
        public InternalVkObjectType Type { get; set; }

        public override string ToString()
        {
            return new {Id, Type}.ToString();
        }
    }
}