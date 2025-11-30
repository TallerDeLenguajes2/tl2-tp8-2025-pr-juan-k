using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PresupuestosController : Controller
{
    private readonly IPresupuestosRepository _presupuestoRepo;
    private readonly IProductosRepository _produtosRepo;
    public PresupuestosController(IPresupuestosRepository presu,IProductosRepository produc)
    {
        _presupuestoRepo = presu;
        _produtosRepo = produc;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var presupuestos = _presupuestoRepo.GetPresupuestos();
        return View(presupuestos);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();  
    }
    [HttpPost]
    public IActionResult Create(PresupuestosViewModel presupuestoNewVM)
    {
        if (!ModelState.IsValid)
        {
            return View(presupuestoNewVM);
        }
        Presupuestos presupuestoNew = new Presupuestos
        {
            NombreDestinatario = presupuestoNewVM.NombreDestinatario,
            FechaCreacion = presupuestoNewVM.FechaCreacion
        };
        _presupuestoRepo.AddPresupuesto(presupuestoNew);
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public IActionResult Edit(int idP)
    {
        var presupuestoEdit = _presupuestoRepo.GetById(idP);
        PresupuestosViewModel PresupuestoVM = new PresupuestosViewModel(presupuestoEdit);
        return View(PresupuestoVM);
    }
    [HttpPost]
    public IActionResult Edit(PresupuestosViewModel presupuestoEditVM)
    {
        if (!ModelState.IsValid)
        {
            return View(presupuestoEditVM);
        }
        Presupuestos presupuestoEdit = new Presupuestos
        {
          idPresupuesto = presupuestoEditVM.idPresupuesto,
          NombreDestinatario = presupuestoEditVM.NombreDestinatario,
          FechaCreacion = presupuestoEditVM.FechaCreacion  
        };
        _presupuestoRepo.Update(presupuestoEdit);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Eliminar(int idP)
    {
        var presupuestoEliminar = _presupuestoRepo.GetById(idP);
        return View(presupuestoEliminar);
    }
    [HttpPost]
    public IActionResult Delete(int IdPresupuesto)
    {
        _presupuestoRepo.Delete(IdPresupuesto);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Details(int idP)
    {
        var presupuestoDetails = _presupuestoRepo.GetById(idP);
        return View(presupuestoDetails);
    }
    [HttpGet]
    public IActionResult AgregarProducto(int idP)
    {
        AgregarProductoViewModel model = new AgregarProductoViewModel
        {
            idPresupuesto = idP,
            ListaProductos =  _produtosRepo.GetAllProductos()
        };
        return View(model);
    }
    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel productoAPresupuestoVM)
    {
        if (!ModelState.IsValid)
        {
            return View(productoAPresupuestoVM);
        }
        _presupuestoRepo.AddDetalle(productoAPresupuestoVM.idPresupuesto,productoAPresupuestoVM.idProducto,productoAPresupuestoVM.Cantidad);
        return RedirectToAction(nameof(Index));
    }
}