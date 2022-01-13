using CVApplication.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVApplication.ViewModels
{
    public class CreateViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Required]
        [ValidateEmailDomain(allowedDomain:"hotmail.com", ErrorMessage = "Email domain must be hotmail.com.")] //same validation done in the AccountController (isEmailExist)
        public string Email { get; set; }
        [Display(Name = "Confirm Email")]
        [Required]
        [Compare("Email", ErrorMessage = "Confirm Email do not match the Email field.")]
        public string ConfirmEmail { get; set; }
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public List<string> Nationality { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public List<string> Skills { get; set; }
        
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!?@#$%^&*_-])[A-Za-z0-9!?@#$%^&*_-]{8,}",
            ErrorMessage = "The Password field must contains letters, numbers, symbols, and at least 8 characters.")]
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "The Number 1 field is required")]
        public int Num1 { get; set; }
        [Required(ErrorMessage = "The Number 2 field is required")]
        public int Num2 { get; set; }
        [Required]
        [Display(Name = ("Verification"))]
        public int VerificationResult { get; set; }

        public List<string> NationalityList { get; set; } = new()
        {
            "Algeria",
            "Bahrain",
            "Djibouti",
            "Egypt",
            "Emirates",
            "Iraq",
            "Jordan",
            "Kuwait",
            "Lebanon",
            "Libya",
            "Mauritania",
            "Morocco",
            "Oman",
            "Palestine",
            "Qatar",
            "KSA",
            "Somalia",
            "Sudan",
            "Syria",
            "Tunisia",
            "Yemen"
        };

        public List<string> SkillList { get; set; } = new()
        {
            "Java",
            "Python",
            "ASP.Net Core"
        };

        public List<string> GenderList { get; set; } = new()
        {
            "Male",
            "Female",
            "Other"
        };
    }
}
