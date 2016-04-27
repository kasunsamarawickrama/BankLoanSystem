using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class CompanyBranchModel
    {
        public Company Company { get; set; }
        public Branch MainBranch { get; set; }
        public IList<Branch> SubBranches { get; set; }
        [Required(ErrorMessage = "Please select the State")]
        [Display(Name ="State")]
        public int StateId { get; set; }

    }

    public class NonRegCompanyBranchModel
    {
        public CompanyBranchModel CompanyBranch { get; set; }

        [Required(ErrorMessage = "Please select the State")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        public List<NonRegBranch> NonRegBranches { get; set; }

        [Required(ErrorMessage = "Please select the Branch")]
        [Display(Name = "Branch Name")]
        public int RegBranchId { get; set; }

        [Required(ErrorMessage = "Please select the Company")]
        [Display(Name = "Company Name")]
        public int NonRegCompanyId { get; set; }

        public Company NonRegCompany { get; set; }
    }
}