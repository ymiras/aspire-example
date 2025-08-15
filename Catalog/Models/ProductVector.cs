using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Models;

public class ProductVector
{
    [VectorStoreKey]
    public int Id { get; set; }
    
    [VectorStoreData]
    public string Name { get; set; } = string.Empty;
    
    [VectorStoreData]
    public string? Description { get; set; }
    
    [VectorStoreData]
    public decimal Price { get; set; }
    
    [VectorStoreData]
    public string ImageUrl { get; set; } = string.Empty;
    
    [NotMapped]
    [VectorStoreVector(Dimensions: 384, DistanceFunction = DistanceFunction.CosineDistance)]
    public ReadOnlyMemory<float> Vector { get; set; }
}