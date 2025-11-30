using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private readonly IPresupuestosRepository _presupuestoRepo;
    public PresupuestosController(IPresupuestosRepository presu)
    {
        _presupuestoRepo = presu;
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
    public IActionResult Create(Presupuestos presupuestoNew)
    {
        _presupuestoRepo.AddPresupuesto(presupuestoNew);
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public IActionResult Edit(int idP)
    {
        var presupuestoEdit = _presupuestoRepo.GetById(idP);
        return View(presupuestoEdit);
    }
    [HttpPost]
    public IActionResult Edit(Presupuestos presupuestoEdit)
    {
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
}