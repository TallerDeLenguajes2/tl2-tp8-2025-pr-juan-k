using Microsoft.AspNetCore.Mvc;

public class ProductosController :Controller
{
    private  IProductosRepository _productoRepository;
    public ProductosController(IProductosRepository produc)
    {
        _productoRepository = produc;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var lista = _productoRepository.GetAllProductos();
        return View(lista);
    }
    [HttpGet]
    public IActionResult CrearProducto()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CrearProducto(Productos productoAdd)
    {
        _productoRepository.AddProducto(productoAdd);
        return RedirectToAction(nameof(Index));
    }
    

}