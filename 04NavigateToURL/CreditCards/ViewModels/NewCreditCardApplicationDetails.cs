using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCards.ViewModels
{
    public class NewCreditCardApplicationDetails
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please provide a first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please provide a last name")]
        public string LastName{ get; set; }

        [Display(Name = "Frequent Flyer Number")]
        [Required(ErrorMessage = "Please provide a frequent flyer number")]
        public string FrequentFlyerNumber { get; set; }

        [Display(Name = "Age (in years)")]
        [Required(ErrorMessage = "Please provide an age in years")]
        [Range(18,int.MaxValue, ErrorMessage = "You must be at least 18 years old")]
        public int? Age { get; set; }

        [Display(Name = "Gross Income")]
        [Required(ErrorMessage = "Please provide your gross income")]        
        public decimal? GrossAnnualIncome { get; set; }

        [Display(Name = "Relationship Status")]
        [Required(ErrorMessage = "Please choose a relationship status")]
        public string RelationshipStatus { get; set; }

        [Display(Name = "How did you hear about us?")]
        [Required(ErrorMessage = "Please select how you heard about us?")]
        public string BusinessSource { get; set; }

        [Display(Name = "Accept terms")]        
        [TrueRequired(ErrorMessage = "You must accept the terms and conditions to continue")]
        public bool TermsAccepted { get; set; }
    }
}
