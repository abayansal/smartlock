using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartLock.Business.Commands;
using SmartLock.Business.Events;
using SmartLock.Business.Exceptions;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Handlers
{
    public class UnlockGateCommandHandler : IRequestHandler<UnlockGateCommand, UnlockGateResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IMediator mediator;

        public UnlockGateCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            this.userRepository = userRepository;
            this.mediator = mediator;
        }

        public async Task<UnlockGateResult> Handle(UnlockGateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new MissingInformationProvidedException("invalid user details provided");
            }

            var user = userRepository.Get(request.UserId);

            if (user == null)
            {
                throw new UserDoesNotExistException("user does not exist");
            }

            var result = new UnlockGateResult(user.HasAccess(request.GateId));

            if (result.IsSuccess)
            {
                var gateUnlockedEvent = new GateUnlockedEvent(request.UserId, request.GateId);
                await mediator.Publish(gateUnlockedEvent, cancellationToken);
            }
            else
            {
                var failedUnlockEvent = new GateUnlockFailedEvent(request.UserId, request.GateId);
                await mediator.Publish(failedUnlockEvent, cancellationToken);
            }

            return result;
        }
    }
}