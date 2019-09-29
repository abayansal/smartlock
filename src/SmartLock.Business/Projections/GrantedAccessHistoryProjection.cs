using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartLock.Business.Events;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Projections
{
    /*
     In a real world scenario, these projections would be different components and most likely implemented in a separate process.
     I skipped to implement it in such a way because, creating a message queue/bus and coding 
     a listener with all success/failure/retry cases and spinning up the message queue would take so long to implement
     */
    public class GrantedAccessHistoryProjection : INotificationHandler<AccessGrantedEvent>
    {
        private readonly IGrantedAccessHistoryRepository grantedAccessHistoryRepository;

        public GrantedAccessHistoryProjection(IGrantedAccessHistoryRepository grantedAccessHistoryRepository)
        {
            this.grantedAccessHistoryRepository = grantedAccessHistoryRepository;
        }

        public Task Handle(AccessGrantedEvent notification, CancellationToken cancellationToken)
        {
            grantedAccessHistoryRepository.Save(notification.UserId, notification.GateId, notification.GrantDate);

            return Task.CompletedTask;
        }
    }
}
