public class Presupuestos
{
    private const decimal IVA = 0.21m; 
    public int IdPresupuesto {get;set;}
    public string?  NombreDestinatario{get;set;}

    public DateTime FechaCreacion {get;set;}

    public List<PresupuestosDetalle> listProdutos {get;set;}
    public Presupuestos()
    {
        listProdutos = new List<PresupuestosDetalle>();
    }
    // <summary>
        /// Calcula el monto total del presupuesto SIN IVA.
        /// </summary>
        /// <returns>Monto total base.</returns>
        public decimal MontoPresupuesto()
        {
            // Se calcula sumando el subtotal de cada detalle (Precio * Cantidad)
            return listProdutos.Sum(d => Convert.ToInt32(d.Producto.Precio) * d.Cantidad);
        }

        /// <summary>
        /// Calcula el monto total del presupuesto CON IVA (21%).
        /// </summary>
        /// <returns>Monto total con IVA incluido.</returns>
        public decimal MontoPresupuestoConIva()
        {
            decimal montoBase = MontoPresupuesto();
            // Retorna el monto base más el 21% del IVA
            return montoBase * (1 + IVA);
        }

        /// <summary>
        /// Cuenta el total de productos sumando las cantidades de todos los ítems.
        /// </summary>
        /// <returns>Cantidad total de unidades de productos.</returns>
        public int CantidadProductos()
        {
            // Suma la propiedad Cantidad de cada elemento en la lista Detalle
            return listProdutos.Sum(d => d.Cantidad);
        }

}