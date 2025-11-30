using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace MVC.ViewModels;

    public class AgregarProductoViewModel
{
        public int idPresupuesto { get; set; }

        [Display(Name = "Producto a agregar")]
        
        [Required(ErrorMessage = "Debe seleccionar un producto.")]
        public int idProducto { get; set; } 

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Cantidad { get; set; }
        public List<Productos>? ListaProductos { get; set; }
    
    // ❗ ❗ LA SOLUCIÓN: IGNORAR ESTA PROPIEDAD EN EL POST ❗ ❗
    // Este SelectList solo se usa para renderizar el Dropdown en la vista GET.
      //  [BindNever]
       // public SelectList? ListaProductos { get; set; } 
    }
