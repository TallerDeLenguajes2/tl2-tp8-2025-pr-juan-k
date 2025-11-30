using System.ComponentModel.DataAnnotations;
using System;


namespace MVC.ViewModels;

    public class PresupuestosViewModel
    {
    public PresupuestosViewModel()
    {
    }

    public PresupuestosViewModel(Presupuestos presupuesto)
    {
        idPresupuesto = presupuesto.idPresupuesto;
        NombreDestinatario = presupuesto.NombreDestinatario;
        FechaCreacion = presupuesto.FechaCreacion;
    }

    public int idPresupuesto { get; set; }

        [Display(Name = "Nombre o Email del Destinatario")]
        [Required(ErrorMessage = "El nombre o email es obligatorio.")]
        public string NombreDestinatario { get; set; } 
            
        [Display(Name = "Fecha de Creación")]
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)] 
        public DateTime FechaCreacion { get; set; } 
    }
