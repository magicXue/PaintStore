using System;

namespace PaintStore.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public string Phone { get; set; }

    //User --- Order 1:N
    public List<Order> Orders { get; set; }
}
