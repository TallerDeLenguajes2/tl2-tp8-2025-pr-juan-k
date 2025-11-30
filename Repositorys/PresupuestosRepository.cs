using Microsoft.Data.Sqlite;

public class PresupuestosRepository :IPresupuestosRepository
{
    private readonly string urlDB = "Data Source=DB/Tienda";
    private readonly ProductosRepository _productoRepo = new ProductosRepository();


    //Agregar
   public void AddPresupuesto(Presupuestos presupuestoAdd)
{
    // 1. Convertir DateOnly a la cadena ISO 8601
    string fechaISO = presupuestoAdd.FechaCreacion.ToString("yyyy-MM-dd");
    
    // Sentencia SQL
    string sentenciaSQL = "INSERT INTO Presupuestos(NombreDestinatario,FechaCreacion) VALUES (@nombre, @fecha)";
    
    using (SqliteConnection connection = new SqliteConnection(urlDB))
    {
        connection.Open();
        using (SqliteCommand command = new SqliteCommand(sentenciaSQL, connection))
        {
            // Agrega parámetros
            command.Parameters.Add(new SqliteParameter("@nombre", presupuestoAdd.NombreDestinatario));
            
            // 2. Usar la cadena formateada en el parámetro
            command.Parameters.Add(new SqliteParameter("@fecha", fechaISO)); 
            
            command.ExecuteNonQuery();
        }
        connection.Close();
    }
}
    //Listapresupuesto
    public List<Presupuestos> GetPresupuestos()
    {
        List<Presupuestos> lista = new List<Presupuestos>();
        string sentenciaSQL = "SELECT * FROM Presupuestos";

        using (SqliteConnection connection = new SqliteConnection(urlDB))
        {
            connection.Open();
            using (SqliteCommand commandDB = new SqliteCommand(sentenciaSQL, connection))
            {
                using (var reader = commandDB.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var presupuesto = new Presupuestos
                        {
                            idPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                            NombreDestinatario = reader.GetString(1),
                            FechaCreacion = reader.GetDateTime(2),
                            // Nota: Los detalles (Detalle) NO se cargan en GetAll() por eficiencia.
                            // Se cargarán solo en GetById() para mantener la lógica síncrona simple.
                        };
                        lista.Add(presupuesto);
                    }
                }

            }
            connection.Close();
        }
        return lista;
    }
    // 2. OBTENER POR ID
    public Presupuestos GetById(int id)
    {
            Presupuestos presupuesto = null;
            const string sql = "SELECT IdPresupuesto, NombreDestinatario, FechaCreacion FROM Presupuestos WHERE IdPresupuesto = @Id";

            using var conexion = new SqliteConnection(urlDB);
            conexion.Open();

            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@Id", id);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                presupuesto = new Presupuestos
                {
                    idPresupuesto = lector.GetInt32(0),
                    NombreDestinatario = lector.GetString(1),
                    FechaCreacion = lector.GetDateTime(2)
                };
                
                // ❗ Lógica Crítica: Cargar los detalles del presupuesto
                presupuesto.listProdutos = GetDetallesByPresupuestoId(id);
            }
            return presupuesto;
    }
    // 4. ACTUALIZAR
    public void Update(Presupuestos presupuesto)
        {
            const string sql = "UPDATE Presupuestos SET NombreDestinatario = @NombreDestinatario, FechaCreacion = @FechaCreacion WHERE IdPresupuesto = @Id";

            using var conexion = new SqliteConnection(urlDB);
            conexion.Open();

            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@NombreDestinatario", presupuesto.NombreDestinatario);
            comando.Parameters.AddWithValue("@FechaCreacion", presupuesto.FechaCreacion);
            comando.Parameters.AddWithValue("@Id", presupuesto.idPresupuesto);

            comando.ExecuteNonQuery();
        }

    // 5. ELIMINAR
    public void Delete(int id)
    {
            // Nota: En una aplicación real, se debería usar una transacción y primero
            // eliminar los detalles (PresupuestoDetalle) para evitar errores de FK.
            
            using var conexion = new SqliteConnection(urlDB);
            conexion.Open();

            // 1. Eliminar los detalles asociados
            const string sqlDetalle = "DELETE FROM PresupuestosDetalle WHERE IdPresupuesto = @Id";
            using var comandoDetalle = new SqliteCommand(sqlDetalle, conexion);
            comandoDetalle.Parameters.AddWithValue("@Id", id);
            comandoDetalle.ExecuteNonQuery();
            
            // 2. Eliminar el presupuesto
            const string sqlPresupuesto = "DELETE FROM Presupuestos WHERE IdPresupuesto = @Id";
            using var comandoPresupuesto = new SqliteCommand(sqlPresupuesto, conexion);
            comandoPresupuesto.Parameters.AddWithValue("@Id", id);
            comandoPresupuesto.ExecuteNonQuery();
    }

    private List<PresupuestosDetalle> GetDetallesByPresupuestoId(int idPresupuesto)
    {
            var detalles = new List<PresupuestosDetalle>();
            const string sql = "SELECT IdPresupuesto, IdProducto, Cantidad FROM PresupuestosDetalle WHERE IdPresupuesto = @IdPresupuesto";

            using var conexion = new SqliteConnection(urlDB);
            conexion.Open();

            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@IdPresupuesto", idPresupuesto);

            using var lector = comando.ExecuteReader();

            // Cargamos todos los productos de la BD de una vez para optimizar
            var todosLosProductos = _productoRepo.GetAllProductos().ToDictionary(p => p.idProducto);
            while (lector.Read())
            {
                int idProducto = lector.GetInt32(1);
                
                // Mapeo
                var detalle = new PresupuestosDetalle
                {
                    // Buscamos el objeto Producto en el diccionario cargado previamente
                    Producto = todosLosProductos.ContainsKey(idProducto) ? todosLosProductos[idProducto] : null,
                    Cantidad = lector.GetInt32(2)
                };
                
                if (detalle.Producto != null)
                {
                    detalles.Add(detalle);
                }
            }

            return detalles;
    }

     public void AddDetalle(int idPresupuesto, int idProducto, int cantidad)
        {
            const string sql = "INSERT INTO PresupuestosDetalle (IdPresupuesto, IdProducto, Cantidad) VALUES (@IdPresupuesto, @IdProducto, @Cantidad)";

            using var conexion = new SqliteConnection(urlDB);
            conexion.Open();

            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@IdPresupuesto", idPresupuesto);
            comando.Parameters.AddWithValue("@IdProducto", idProducto);
            comando.Parameters.AddWithValue("@Cantidad", cantidad);

            comando.ExecuteNonQuery();
        }


}