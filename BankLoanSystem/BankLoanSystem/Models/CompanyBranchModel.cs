using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class CompanyBranchModel
    {
        public Company Company { get; set; }
        public Branch MainBranch { get; set; }

        [Required]
        [Display(Name ="State")]
        public int StateId { get; set; }

    }
}