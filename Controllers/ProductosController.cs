using Microsoft.AspNetCore.Mvc;

public class ProductosController : Controller
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
    public IActionResult CrearProducto(ProductosViewModels productoAdd)
    {
        if (!ModelState.IsValid)
        {
            return View(productoAdd);
        }
        Productos creado = new Productos{
            Descripcion = productoAdd.Descripcion,
            Precio = productoAdd.Precio

        };
        _productoRepository.AddProducto(creado);
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
        ProductosViewModels edit = new ProductosViewModels(productSelecionado);

        if (productSelecionado == null)
        {
            return NotFound();
        }

        return View(edit);        
    }
    [HttpPost]
    public IActionResult Modificar(ProductosViewModels producModif)
    {
        Productos modif = new Productos(producModif.idProducto,producModif.Descripcion,producModif.Precio);

        
        _productoRepository.Update(modif);
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