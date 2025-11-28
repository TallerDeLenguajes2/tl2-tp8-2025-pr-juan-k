using Microsoft.Data.Sqlite;

public class ProductosRepository
{
    private readonly string urlDB  = "Data Source=DB/Tienda";

    //nuevo producto a la DB
    public void AddProducto(Productos productoAdd)
    {
        string sentenciaSQL = "INSERT INTO Porductos(Descripcion,Precio) VALUES (@descripcion,@precio)";
        using (SqliteConnection connection = new SqliteConnection(urlDB))
        {
            connection.Open();
            using (SqliteCommand commandDB = new SqliteCommand(sentenciaSQL,connection))
            {
                //vinculo parametros a la sentencia
                commandDB.Parameters.AddWithValue("@descripcion",productoAdd.Descripcion);
                commandDB.Parameters.AddWithValue("@Precio",productoAdd.Precio);
                //ejecuto sentencia
                commandDB.ExecuteReader();
            }
            connection.Close();
        }
    }


}