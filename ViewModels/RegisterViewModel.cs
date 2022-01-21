using System.ComponentModel.DataAnnotations;

namespace register.ViewModels;

public class RegisterViewModel : IValidatableObject
{
    public string ReturnUrl { get; set; }
    
    [Required(ErrorMessage = "Enter the fullname")]
    [Display(Name = "Fullname")]
    public string Fullname { get; set; }

    public string Username{ get; set;}

    [Required(ErrorMessage = "Enter date of birth")]
    [Display(Name = "Date of birth")]
    public DateTimeOffset Birthdate { get; set; }
    
    [Required(ErrorMessage = "Enter the phonenumber")]
    [RegularExpression(@"^[\+]?(998[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{3}[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{2}[-\s\.]?)$",
    ErrorMessage = "The phone number format is not correct.")]
    [Display(Name = "Phonenumber")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "Enter the email")]
    [EmailAddress(ErrorMessage = "The email adress is not correct")]
    [Display(Name = "Your email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Enter the password")]
    [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm passcode")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match, please check again.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
    
    [Display(Name = "Remember")]
    public bool RememberMe { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var now = DateTimeOffset.Now;
        var limit = new DateTime(now.Year - 13, now.Month, now.Day);
        Console.WriteLine($"{limit} {Birthdate}");

        if(Birthdate > limit)
        {
            yield return new ValidationResult($"You must be over 13 years old!", new [] { nameof(Birthdate)});
        }
    }
}