namespace Catalog.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}

// dotnet ef migrations add --project Catalog\Catalog.csproj
// --startup-project Catalog\Catalog.csproj --context Catalog.Data.ProductDbContext
// --configuration Debug --no-build InitialCreate --output-dir Data\Migrations