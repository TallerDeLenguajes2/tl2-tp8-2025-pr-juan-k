public class Productos
{
    public int idProducto{get;set;}
    public string? Descripcion{get;set;}
    public float Precio {get;set;}
    public Productos()
    {
        
    }
    public Productos(int idP,string desP,float preP)
    {
        idProducto = idP;
        Descripcion = desP;
        Precio = preP;
    }

}