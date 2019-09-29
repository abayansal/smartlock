using System.Collections.Generic;
using System.Linq;
using SmartLock.Domain;

namespace SmartLock.Persistence.Entities
{
    public class UserEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identity { get; set; }
        public List<string> GrantedAccessList { get; set; }

        public User AsDomainObject()
        {
            var user = new User(FirstName, LastName, Identity);

            foreach (var access in GrantedAccessList)
            {
                user.GrantedAccessList.Add(access);
            }

            return user;
        }

        public static UserEntity Load(User user)
        {
            return new UserEntity
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Identity = user.Identity,
                GrantedAccessList = user.GrantedAccessList.ToList()
            };
        }
    }
}