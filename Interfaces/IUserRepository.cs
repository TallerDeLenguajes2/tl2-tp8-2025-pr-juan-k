using MVC.Models;

public interface IUserRepository
{
     public Usuario GetUser(string usuario, string contrasena);
}