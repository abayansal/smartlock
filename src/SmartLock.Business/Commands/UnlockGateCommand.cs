using MediatR;

namespace SmartLock.Business.Commands
{
    public class UnlockGateCommand : IRequest<UnlockGateResult>
    {
        public string UserId { get; }
        public string GateId { get; }

        public UnlockGateCommand(string userId, string gateId)
        {
            UserId = userId;
            GateId = gateId;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(UserId) && !string.IsNullOrWhiteSpace(GateId);
        }
    }

    public class UnlockGateResult : IRequest
    {
        public UnlockGateResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }
    }
}