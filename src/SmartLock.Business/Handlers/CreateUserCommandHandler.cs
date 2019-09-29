using System;
using MediatR;
using SmartLock.Business.Commands;
using SmartLock.Domain;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Handlers
{
    public class CreateUserCommandHandler : RequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        protected override void Handle(CreateUserCommand request)
        {
            if (!request.IsValid())
            {
                throw new Exception("invalid user details provided");
            }
            var user = new User(request.FirstName, request.LastName, request.Identity);

            userRepository.Save(user);
        }
    }
}
