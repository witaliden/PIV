using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI_0508;

[Table("Klient")]
public class Client
{
    public Client()
    {
        Orders = new List<Order>();
    }

    public int Id { get; set; }
    //Metoda za pomocą atrybutów
    [Required]
    public string Name { get; set; }
    

    public List<Order> Orders { get; set; }
}