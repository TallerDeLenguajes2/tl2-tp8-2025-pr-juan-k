public interface IPresupuestosRepository
{
    public void AddPresupuesto(Presupuestos presupuestoAdd);
    public List<Presupuestos> GetPresupuestos();
    public Presupuestos GetById(int id);
     public void Update(Presupuestos presupuesto);
     public void Delete(int id);

     //un metodo no agregar
     public void AddDetalle(int idPresupuesto, int idProducto, int cantidad);
     
   
}