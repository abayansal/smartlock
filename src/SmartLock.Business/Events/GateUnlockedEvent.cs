using System;
using MediatR;

namespace SmartLock.Business.Events
{
    public class GateUnlockedEvent : INotification
    {
        public string UserId { get; }
        public string GateId { get; }
        public DateTime AttemptDate { get; }

        public GateUnlockedEvent(string userId, string gateId)
        {
            UserId = userId;
            GateId = gateId;
            AttemptDate = DateTime.UtcNow;
        }
    }
}