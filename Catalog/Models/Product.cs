﻿namespace Catalog.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    public string ImageUrl { get; set; } = string.Empty;
}