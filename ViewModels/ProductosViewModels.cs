using System.ComponentModel.DataAnnotations;

public class ProductosViewModels
{
    
    public int idProducto{get;set;}

    [Required(ErrorMessage = "Porfavor ingrese una descripcion")]
    [StringLength(100,ErrorMessage = "La descripcion debe tener un maximo de 100 caracteres")]
    [Display(Name ="Descripcion del producto")]
    public string? Descripcion{get;set;}

    
    [Required(ErrorMessage = "Porfavor ingrese un precio")]
    [Display(Name = "Precio del producto")]
    [Range(0.01,float.MaxValue,ErrorMessage = "El precio debe ser mayor a 0.01")]
    public float Precio {get;set;}
}