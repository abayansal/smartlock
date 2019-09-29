using SmartLock.Domain;

namespace SmartLock.Persistence.Entities
{
    public class GateEntity
    {
        public string Identity { get; set; }
        public string Description { get; set; }

        public Gate AsDomainObject()
        {
            return new Gate(Identity, Description);
        }

        public static GateEntity Load(Gate gate)
        {
            return new GateEntity
            {
                Identity = gate.Identity,
                Description = gate.Description
            };
        }
    }
}