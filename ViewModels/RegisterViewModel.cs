namespace register.ViewModels;

public class RegisterViewModel
{
    public string ReturnUrl { get; set; }
    
    public string Fullname { get; set; }
    
    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string RememberMe { get; set; }
    
    public string Email { get; set; }

    public string Username{ get; set;}

    public string Phone { get; set; }

    public DateTimeOffset Birthdate { get; set; }
}