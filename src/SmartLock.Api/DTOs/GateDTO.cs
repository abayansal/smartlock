using System.ComponentModel.DataAnnotations;

namespace SmartLock.Api.DTOs
{
    public class GateDTO
    {
        [Required]
        public string Identity { get; set; }
        public string Description { get; set; }
    }
}