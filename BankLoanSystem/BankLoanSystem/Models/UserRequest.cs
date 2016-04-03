using System;
using System.ComponentModel.DataAnnotations;

namespace BankLoanSystem.Models
{
    public class UserRequest
    {

        [Required]
        [Display(Name = "User Id")]
        public int request_id { get; set; }

        [Required]
        [Display(Name = "Company Id")]
        public int company_id { get; set; }

        [Required]
        [Display(Name = "Branch Id")]
        public int branch_id { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int user_id { get; set; }

        [Required]
        [Display(Name = "Role id")]
        public int role_id { get; set; }

        [Required]
        [Display(Name = "Loan Code")]
        public string loan_code { get; set; }

        [Required]
        [Display(Name = "Page Name")]
        public string page_name { get; set; }

        public string topic { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [Display(Name = "Message")]
        public string message { get; set; }

        public string priority_level { get; set; }

        [Required]
        [Display(Name = "Message Date")]
        public DateTime message_date { get; set; }

        public string answer { get; set; }

        public int answer_user_id { get; set; }

        public DateTime answer_date { get; set; }

        [Required]
        [Display(Name = "Is Answer")]
        public bool is_answer { get; set; }

        [Required]
        [Display(Name = "Is View Reply")]
        public bool is_rep_view { get; set; }


    }
}