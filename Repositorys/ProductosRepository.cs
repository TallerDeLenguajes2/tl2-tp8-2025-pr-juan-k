using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Data.Sqlite;

public class ProductosRepository : IProductosRepository
{
    private readonly string urlDB  = "Data Source=DB/Tienda";

    //nuevo producto a la DB
    public void AddProducto(Productos productoAdd)
    {
        string sentenciaSQL = "INSERT INTO Productos(Descripcion,Precio) VALUES (@descripcion,@precio)";
        using (SqliteConnection connection = new SqliteConnection(urlDB))
        {
            connection.Open();
            using (SqliteCommand commandDB = new SqliteCommand(sentenciaSQL,connection))
            {
                //vinculo parametros a la sentencia
                commandDB.Parameters.AddWithValue("@descripcion",productoAdd.Descripcion);
                commandDB.Parameters.AddWithValue("@precio",productoAdd.Precio);
                //ejecuto sentencia
                commandDB.ExecuteReader();
            }
            connection.Close();
        }
    }
    //Actualizar
    public bool Update(Productos productoUpda)
    {
        int encontrado = 0;
        string sentenciaSQL = "UPDATE Productos SET Descripcion = @descripcion,Precio = @precio WHERE idProducto = @idP";
        using (SqliteConnection connectionDB = new SqliteConnection(urlDB))
        {
            connectionDB.Open();
            using (SqliteCommand commandDB = new SqliteCommand(sentenciaSQL,connectionDB))
            {
                commandDB.Parameters.Add(new SqliteParameter("idP",productoUpda.idProducto));
                commandDB.Parameters.Add(new SqliteParameter("descripcion",productoUpda.Descripcion));
                commandDB.Parameters.Add(new SqliteParameter("Precio",productoUpda.Precio));
                encontrado = commandDB.ExecuteNonQuery();
            }
            connectionDB.Close();
        }
        return encontrado >0;

    }
    //lista de productos
    public List<Productos> GetAllProductos()
    {
        List<Productos> productosLista  = new List<Productos>(); 
        string sentenciaSQL = "SELECT * FROM Productos";
        using (SqliteConnection connectionDB = new SqliteConnection(urlDB))
        {
            connectionDB.Open();
            using (SqliteCommand commandDB = new SqliteCommand(sentenciaSQL,connectionDB))
            {
                using (var reader = commandDB.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Productos product = new Productos
                        {
                          idProducto = reader.GetInt32(0),
                          Descripcion = reader.GetString(1),
                          Precio = reader.GetFloat(2)  
                        };
                        productosLista.Add(product);
                    }   
                }
            }
            connectionDB.Close();
        }
        return productosLista;
    }
    //buscar 1 producto
    public Productos Buscar(int idP)
    {
        Productos productoEncontrado = null;
        string sentenciaSQl = "SELECT * FROM Poductos WHERE idProducto = @idP";
        using (SqliteConnection connectionDB = new SqliteConnection(urlDB))
        {
            connectionDB.Open();
            using (SqliteCommand commandDB = new SqliteCommand(sentenciaSQl,connectionDB))
            {
                commandDB.Parameters.Add(new SqliteParameter("idP",idP));
                using (var encontrado = commandDB.ExecuteReader())
                {
                    if (encontrado.Read())
                    {
                        productoEncontrado = new Productos();
                        productoEncontrado.idProducto = encontrado.GetInt32(0);
                        productoEncontrado.Descripcion = encontrado.GetString(1);
                        productoEncontrado.Precio = encontrado.GetFloat(2);

                    }
                }     
            }
            connectionDB.Close();
        }
        return productoEncontrado;
    }
    //delete
    public bool DeleteProducto(int idP)
    {
        int eliminado = 0;
        string sentenciaSQL = "DELETE FROM Productos WHERE idProducto = @idP";
        using (SqliteConnection connectionDB = new SqliteConnection(urlDB))
        {
            connectionDB.Open();
            using (SqliteCommand commandDB = new SqliteCommand(sentenciaSQL,connectionDB))
            {
                commandDB.Parameters.Add(new SqliteParameter("idP",idP));
                eliminado = commandDB.ExecuteNonQuery();
            }
            connectionDB.Close();
            
        }
        return eliminado >0;
    }

}