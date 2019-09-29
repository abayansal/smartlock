using MediatR;

namespace SmartLock.Business.Commands
{
    public class CreateGateCommand : IRequest
    {
        public string Identity { get; }
        public string Description { get; }

        public CreateGateCommand(string identity, string description)
        {
            Identity = identity;
            Description = description;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Identity);
        }
    }
}
