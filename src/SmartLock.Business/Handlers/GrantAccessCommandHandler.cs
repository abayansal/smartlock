using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartLock.Business.Commands;
using SmartLock.Business.Events;
using SmartLock.Business.Exceptions;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Handlers
{
    public class GrantAccessCommandHandler : IRequestHandler<GrantAccessCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IGateRepository gateRepository;
        private readonly IMediator mediator;

        public GrantAccessCommandHandler(IUserRepository userRepository, IGateRepository gateRepository, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.gateRepository = gateRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(GrantAccessCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new MissingInformationProvidedException("invalid user details provided");
            }

            var user = userRepository.Get(request.UserId);
            if (user == null)
            {
                throw new UserDoesNotExistException($"no gate with id {request.UserId}");
            }


            if (gateRepository.Get(request.GateId) == null)
            {
                throw new GateDoesNotExistException($"no gate with id {request.GateId}");
            }

            user.GrantAccess(request.GateId);

            userRepository.Save(user);

            var accessGrantedEvent = new AccessGrantedEvent(request.UserId, request.GateId);
            await mediator.Publish(accessGrantedEvent, cancellationToken);

            return Unit.Value;
        }
    }
}