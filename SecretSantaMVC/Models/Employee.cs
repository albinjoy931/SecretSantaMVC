using System.ComponentModel.DataAnnotations;

namespace SecretSantaMVC.Models
{
    public class Employee
    {
        [Required]
        public string Employee_Name { get; set; }

        [Required, EmailAddress]
        public string Employee_EmailID { get; set; }
    }
}
