using System.ComponentModel.DataAnnotations.Schema;

namespace FluentAPI_0508;

public class Order
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    
    //[ForeignKey("Client")]
    public int ClientId { get; set; }
    public Client Client { get; set; }
}