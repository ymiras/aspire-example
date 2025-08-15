namespace Catalog.Services;

public class ProductAiService(
    ProductDbContext dbContext,
    IChatClient chatClient,
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
    VectorStoreCollection<int, ProductVector> productVectorCollection)
{
    public async Task<string> SupportAsync(string query)
    {
        var systemPrompt = """
                           You are a useful assistant.
                           You always reply with a short and funny message.
                           If you do not know an answer, you say 'I don't know that.'
                           You only answer questions related to outdoor camping products.
                           For any other type of questions, explain to the user that you only answer outdoor campi
                           At the end, Offer one of our products: Hiking Poles-$24, 0utdoor Rain Jacket-$12, Outdc
                           Do not store memory of the chat conversation
                           """;
        
        var chatHistory = new List<ChatMessage>
        {
            new(ChatRole.System, systemPrompt),
            new(ChatRole.User, query)
        };
        
        var resultPrompt = await chatClient.GetResponseAsync(chatHistory);
        return resultPrompt.Messages.First().Text;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
    {
        if (!await productVectorCollection.CollectionExistsAsync())
        {
            await InitialEmbeddingsAsync();
        }

        var queryEmbedding = await embeddingGenerator.GenerateVectorAsync(query);
        var results = productVectorCollection.SearchAsync(
            queryEmbedding, top: 2,
            options: new VectorSearchOptions<ProductVector>
            {
                VectorProperty = r => r.Vector
            }
       );

        List<Product> products = [];

        await foreach (var result in results)
        {
            products.Add(new Product
            {
                Id = result.Record.Id,
                Name = result.Record.Name,
                Description = result.Record.Description,
                ImageUrl = result.Record.ImageUrl,
                Price = result.Record.Price,
            });
        }
        
        return products;
    }

    private async Task InitialEmbeddingsAsync()
    {
        await productVectorCollection.EnsureCollectionExistsAsync();
        
        var products = await dbContext.Products.ToListAsync();

        foreach (var product in products)
        {
            var productInfo = $"{product.Name} is a product that cost [{product.Price}] and [{product.Description}]";

            var productVector = new ProductVector
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Vector = await embeddingGenerator.GenerateVectorAsync(productInfo)
            };

            await productVectorCollection.UpsertAsync(productVector);
        }
    }
}