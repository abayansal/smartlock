using System.Collections.Generic;

namespace SmartLock.Domain
{
    public class User
    {
        public string FirstName { get;}

        public string LastName { get;}

        public string Identity { get;}

        public HashSet<string> GrantedAccessList { get; }

        public User(string firstName, string lastName, string identity)
        {
            FirstName = firstName;
            LastName = lastName;
            Identity = identity;
            GrantedAccessList = new HashSet<string>();
        }


        //very simple domain logic below
        public bool HasAccess(string gateId)
        {
            return GrantedAccessList.Contains(gateId);
        }

        public void GrantAccess(string gateId)
        {
            if (!GrantedAccessList.Contains(gateId))
            {
                GrantedAccessList.Add(gateId);
            }
        }
    }
}