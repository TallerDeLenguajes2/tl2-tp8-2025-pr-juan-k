using System.ComponentModel.DataAnnotations;
namespace MVC.ViewModels;
public class LoginViewModel
{
    [Display(Name = "ingrese usuario")]
    [Required(ErrorMessage = "Ingrese su NOMBRE de usuario porfavor!")]
    public string ?Username { get; set; }
    
    [Display(Name = "Nombre de usuario")]
    [Required(ErrorMessage = "Ingrese su nombre de usuario porfavor")]
    public string ?Password { get; set; }
    public string ?ErrorMessage { get; set; }
    public bool ?IsAuthenticated { get; set; } 
}
