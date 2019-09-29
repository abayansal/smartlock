using System.ComponentModel.DataAnnotations;

namespace SmartLock.Api.DTOs
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Identity { get; set; }
    }
}
