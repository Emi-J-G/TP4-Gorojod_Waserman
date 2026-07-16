namespace TP04_Gorojod_Waserman.Models;
using Microsoft.Data.SqlClient;
using Dapper;

public class BD 
{   
    private string connectionString = @"Server=localhost;DataBase=Album;Integrated Security=True;TrustServerCertificate=True;";
    private string query = "";

    public int getCantTotal(){
        int cantTotal;
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            string query = "SELECT COUNT(id) FROM Figuritas;";
            cantTotal = connection.QueryFirstOrDefault<int>(query);
        }
        return cantTotal;
    }

    public List<Figuritas> getFiguritas(){
        List<Figuritas> figuritas = new List<Figuritas>();
        string query;
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            query = "SELECT * FROM Figuritas LEFT JOIN Album ON Figuritas.id = Album.idFigurita;";
            figuritas = connection.Query<Figuritas>(query).ToList();
        }
        return figuritas;
    }

    public List<Figuritas> abrirSobre(){
        List<Figuritas> figuritas = new List<Figuritas>();
        string query;
        Random random = new Random();
        int cantidadTotal = getCantTotal();
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            for (int i = 0; i < 5; i++) {
                int numRandom = random.Next(1, cantidadTotal+1); 
                query = "SELECT * FROM Figuritas WHERE Id = @numRandom;";
                figuritas.Add((connection.QueryFirstOrDefault<Figuritas>(query, new {numRandom})));
            }
        }
        return figuritas;
    }
    public void añadirFiguritas (List<Figuritas> figuritas){
        using (SqlConnection connection = new SqlConnection(connectionString)){
            Album album = new Album();
            string query;
            int resultado;
            for (int i = 0; i <= figuritas.Count; i++){
                query = "SELECT Figuritas.numero FROM Album INNER JOIN Figuritas ON Album.idFigurita = Figuritas.id WHERE Figuritas.id = '@id';";
                resultado = connection.QueryFirstOrDefault<int>(query, new {id = figuritas[i].id});
                if (resultado == null){
                    query = "INSERT Album(id, archivo, nombre, numero, pais, color) VALUES (@id, @archivo, @nombre, @numero, @pais, @color);";
                    connection.Execute(query, new {id = figuritas[i].id, archivo = figuritas[i].archivo, nombre = figuritas[i].nombre, numero = figuritas[i].numero, pais = figuritas[i].pais, color = figuritas[i].color});
                    album.getPegadas().Add(figuritas[i]);
                }
                else {
                    query = "UPDATE Album SET cantidad+=1 WHERE idFigurita=@id";
                    connection.Execute(query, new {id = figuritas[i].id});
                }
            }
        }
    }    
}