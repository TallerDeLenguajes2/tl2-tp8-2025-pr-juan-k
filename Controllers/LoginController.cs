using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;

public class LoginController :Controller
{
    private readonly IAuthenticationService _authenticationRepo;

    public LoginController(IAuthenticationService auten)
    {
        _authenticationRepo = auten;
    }

    [HttpGet]
    public IActionResult Index()
    {
        LoginViewModel modelo = new LoginViewModel();
        return View(modelo);
    }
    [HttpPost]
    public IActionResult Index(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.ErrorMessage = "¡Ingrese sus datos porfavor!";
            return View(model);
        }
        if (_authenticationRepo.Login(model.Username,model.Password))
        {
            return RedirectToAction("Index","Home");
        }
        model.ErrorMessage ="¡Acceso denegado!";
        return View(model);
    }
    [HttpGet]
    public IActionResult Logout()
    {
        _authenticationRepo.Logout();
        return RedirectToAction("Index");
    }
}