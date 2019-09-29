using MediatR;

namespace SmartLock.Business.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identity { get; set; }

        public CreateUserCommand(string firstName, string lastName, string identity)
        {
            FirstName = firstName;
            LastName = lastName;
            Identity = identity;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(Identity);
        }
    }
}
