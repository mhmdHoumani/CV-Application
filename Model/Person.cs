using System;
using System.ComponentModel.DataAnnotations;

namespace CVApplication.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Skills { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhotoPath { get; set; }
    }
}
