using System;

namespace SmartLock.Persistence.Entities
{
    public class UnlockGateAttemptEntity
    {
        public string UserId { get; set; }
        public string GateId { get; set; }
        public DateTime AttemptDate { get; set; }
        public bool WasSuccess { get; set; }
    }
}