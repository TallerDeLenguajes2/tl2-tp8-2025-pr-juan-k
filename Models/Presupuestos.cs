public class Presupuestos
{
    public int IdPresupuesto {get;set;}
    public string?  NombreDestinatario{get;set;}

    public DateOnly FechaCreacion {get;set;}

    public List<PresupuestosDetalle> listProdutos {get;set;}
    public Presupuestos()
    {
        listProdutos = new List<PresupuestosDetalle>();
    }

}