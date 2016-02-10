using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class CompanyBranchModel
    {
        public Company Company { get; set; }
        public Branch MainBranch { get; set; }
        public IList<Branch> SubBranches { get; set; }
        [Required]
        [Display(Name ="State")]
        public int StateId { get; set; }

    }

    public class NonRegCompanyBranchModel
    {
        public CompanyBranchModel CompanyBranch { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public List<NonRegBranch> NonRegBranches { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public int RegBranchId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int NonRegCompanyId { get; set; }
    }
}