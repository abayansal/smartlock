using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartLock.Business.Events;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Projections
{
    public class UnsuccessfulAttemptHistoryProjection : INotificationHandler<GateUnlockFailedEvent>
    {
        private readonly IUnlockGateAttemptHistoryRepository unlockGateAttemptHistoryRepository;

        public UnsuccessfulAttemptHistoryProjection(IUnlockGateAttemptHistoryRepository unlockGateAttemptHistoryRepository)
        {
            this.unlockGateAttemptHistoryRepository = unlockGateAttemptHistoryRepository;
        }

        public Task Handle(GateUnlockFailedEvent notification, CancellationToken cancellationToken)
        {
            unlockGateAttemptHistoryRepository.Save(notification.UserId, notification.GateId, notification.AttemptDate, false);

            return Task.CompletedTask;
        }
    }
}