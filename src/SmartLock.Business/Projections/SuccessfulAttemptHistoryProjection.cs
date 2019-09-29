using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartLock.Business.Events;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Projections
{
    public class SuccessfulAttemptHistoryProjection : INotificationHandler<GateUnlockedEvent>
    {
        private readonly IUnlockGateAttemptHistoryRepository unlockGateAttemptHistoryRepository;

        public SuccessfulAttemptHistoryProjection(IUnlockGateAttemptHistoryRepository unlockGateAttemptHistoryRepository)
        {
            this.unlockGateAttemptHistoryRepository = unlockGateAttemptHistoryRepository;
        }

        public Task Handle(GateUnlockedEvent notification, CancellationToken cancellationToken)
        {
            unlockGateAttemptHistoryRepository.Save(notification.UserId, notification.GateId, notification.AttemptDate, true);

            return Task.CompletedTask;
        }
    }
}