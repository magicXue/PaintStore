using System;

namespace PaintStore.Models.DTOs;

public class OrderCreateDto
{
    public int UserId { get; set; }

    //User Email can be optional, only required when new user register
    public string UserEmail { get; set; }

    //User Name can be optional, only required when new user register
    public string UserName { get; set; }

    public bool IsNewUser { get; set; }

    public List<int> PaintProductIds { get; set; }
}
