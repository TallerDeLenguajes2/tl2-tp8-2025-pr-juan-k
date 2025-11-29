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
    [HttpGet]
    public IActionResult Detalle(int idP)
    {
        var productos = _productoRepository.Buscar(idP);
    
        return View(productos);
    }
    [HttpGet]
    public IActionResult Modificar(int idP)
    {
        var productSelecionado = _productoRepository.Buscar(idP);
        return View(productSelecionado);        
    }
    [HttpPost]
    public IActionResult Modificar(Productos producModif)
    {
        _productoRepository.Update(producModif);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Eliminar(int idP)
    {
        var encontrado = _productoRepository.Buscar(idP);
        return View(encontrado);
    }
    [HttpGet]
    public IActionResult EliminarConfirm(int idP)
    {
        _productoRepository.DeleteProducto(idP);
        return RedirectToAction("Index");        
    }

    

}