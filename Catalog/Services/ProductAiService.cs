namespace Catalog.Services;

public class ProductAiService(IChatClient chatClient)
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
}