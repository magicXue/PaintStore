using System;

namespace PaintStore.Models;

public class Order
{
    public int Id { get; set; }

    //Order to PaintProduct  1:N
    //EF will apply relation based on List
    public List<PaintProduct> PaintProducts { get; set; }

    //Order ---> User   N:1
    public User User { get; set; }
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    //Total Price should be a auto commputed value from PaintProducts
    public decimal TotalPrice { get => PaintProducts.Sum(p=>p.Price);}
    
    public Order()
    {
        PaintProducts = new List<PaintProduct>(); 
        CreatedAt = DateTime.Now;
    }
}
