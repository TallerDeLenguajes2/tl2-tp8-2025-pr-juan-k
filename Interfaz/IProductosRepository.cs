public interface IProductosRepository
{
    public void AddProducto(Productos productoAdd);
    public bool Update(Productos productoUpda);
    public List<Productos> GetAllProductos();
    public Productos Buscar(int idP);
    public bool DeleteProducto(int idP);
}