using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintStore.Models;

public class PaintProduct
{
    //convention based PK
    public int Id { get; set; }
    
    public string Name { get; set; }

    public decimal Price { get; set; }

    //PaintProduct ------ Order   N:1
    //We are setting 1:N to order relationship 

    //Many to many relation
    //SQL ----- Joint Table 中间表 ------ 本不是源生支持多对多关系， 
    // User ------  Order -------     XXX     User -- XXX table 1:N,     Order -- XXX table 1:N, 


    //Navigation Property
    public List<Order> Orders { get; set; }

    public PaintProduct(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
