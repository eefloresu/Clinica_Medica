using System.ComponentModel.DataAnnotations;

namespace Sistema_ClinicaMedica.Models
{
    public class Users {
        [Required]
        public String user { get; set; }
        [Required]
        public String pass { get; set; }
    }
}
