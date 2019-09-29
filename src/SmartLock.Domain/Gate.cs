namespace SmartLock.Domain
{
    public class Gate
    {
        public string Identity { get; }
        public string Description { get; }

        public Gate(string identity, string description)
        {
            Identity = identity;
            Description = description;
        }
    }
}