namespace TP04_Gorojod_Waserman.Models;

public class Album 
{
    public int id {get; set;}
    public int idFigurita {get; set;}
    public int cantidad {get; set;}
    private List<Figuritas> pegadas = new List<Figuritas>();
    
    
    public Album() {
    }

    public List<Figuritas> getPegadas() {
        return pegadas;
    }
    
}
