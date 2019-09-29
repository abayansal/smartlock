using System;
using MediatR;

namespace SmartLock.Business.Events
{
    public class AccessGrantedEvent : INotification
    {
        public string UserId { get; }
        public string GateId { get; }
        public DateTime GrantDate { get; }

        public AccessGrantedEvent(string userId, string gateId)
        {
            UserId = userId;
            GateId = gateId;
            GrantDate = DateTime.UtcNow;
        }
    }
}
