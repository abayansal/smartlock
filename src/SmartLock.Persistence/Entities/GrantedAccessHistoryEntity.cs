using System;

namespace SmartLock.Persistence.Entities
{
    public class GrantedAccessHistoryEntity
    {
        public string UserId { get; set; }
        public string GateId { get; set; }
        public DateTime GrantDate { get; set; }
    }
}
