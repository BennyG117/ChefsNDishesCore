#pragma warning disable CS8618
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// Add this using statement to access NotMapped
using System.ComponentModel.DataAnnotations.Schema;


namespace ChefsNDishesCore.Models;
public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    //Will require its own class - see below - look at custom validations from LEARN*
    [DateOfBirthInPast]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }


    //created/updated at  ======================== 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



    //(REQUIRED TO MAKE "INCLUDE" POSSIBLE) list of Dish objects (adding to an empty list of Dish objects):
    public List<Dish> ChefDishes { get; set; } = new List<Dish>();


}
// Add new class for DOB Validation here*
public class DateOfBirthInPastAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime Now = DateTime.Now;
        DateTime Input = (DateTime)value;


        if (Input > Now)
        {
            return new ValidationResult("Date of Birth must be in the past.");
        } else {
            return ValidationResult.Success;
        }
    }
}


//  ============================================
//? Setting up Login & Registration:
//  ============================================
// [Required]
// [EmailAddress]
// //Adding unique
// [UniqueEmail]
// public string Email { get; set; }        

//?password  ======================== 
// [Required]
// [DataType(DataType.Password)]
// [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
// public string Password { get; set; }          


//?Password confirm  ======================== 
// // This does not need to be moved to the bottom
// // But it helps make it clear what is being mapped and what is not
// [NotMapped]
// // There is also a built-in attribute for comparing two fields we can use!
// [Compare("Password")]
// [DataType(DataType.Password)]
// public string PasswordConfirm { get; set; }







//? Adding Unique ======================== 
// public class UniqueEmailAttribute : ValidationAttribute
// {
//     protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//     {
//     	// Though we have Required as a validation, sometimes we make it here anyways
//     	// In which case we must first verify the value is not null before we proceed
//         if(value == null)
//         {
//     	    // If it was, return the required error
//             return new ValidationResult("Email is required!");
//         }

//     	// This will connect us to our database since we are not in our Controller
//         MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
//         // Check to see if there are any records of this email in our database
//     	if(_context.Users.Any(e => e.Email == value.ToString()))
//         {
//     	    // If yes, throw an error
//             return new ValidationResult("Email must be unique!");
//         } else {
//     	    // If no, proceed
//             return ValidationResult.Success;
//         }
//     }
// }

