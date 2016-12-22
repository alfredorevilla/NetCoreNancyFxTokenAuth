using System.ComponentModel.DataAnnotations;

namespace NancyWebApp.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}