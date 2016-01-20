using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class UserRole
    {
        [Required]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}