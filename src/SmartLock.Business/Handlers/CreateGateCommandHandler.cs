using System;
using MediatR;
using SmartLock.Business.Commands;
using SmartLock.Domain;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Business.Handlers
{
    public class CreateGateCommandHandler : RequestHandler<CreateGateCommand>
    {
        private readonly IGateRepository gateRepository;

        public CreateGateCommandHandler(IGateRepository gateRepository)
        {
            this.gateRepository = gateRepository;
        }


        protected override void Handle(CreateGateCommand request)
        {
            if (!request.IsValid())
            {
                throw new Exception("invalid gate details provided");
            }
            var gate = new Gate(request.Identity, request.Description);

            gateRepository.Save(gate);
        }
    }
}