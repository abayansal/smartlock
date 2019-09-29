using MediatR;

namespace SmartLock.Business.Commands
{
    public class GrantAccessCommand : IRequest
    {
        public string UserId { get; set; }
        public string GateId { get; set; }

        public GrantAccessCommand(string userId, string gateId)
        {
            UserId = userId;
            GateId = gateId;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(UserId) && !string.IsNullOrWhiteSpace(GateId);
        }
    }
}