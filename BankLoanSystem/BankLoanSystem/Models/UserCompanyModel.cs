using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class UserCompanyModel
    {
        public User User { get; set; }
        public Company Company { get; set; }
        public Branch Branch { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }
    }
}