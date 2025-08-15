var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductAiService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

// Register Ollama-based chat & embedding 
builder
    .AddOllamaApiClient(connectionName: "ollama-llama3-2")
    .AddChatClient();

builder
    .AddOllamaApiClient(connectionName: "ollama-all-minilm")
    .AddEmbeddingGenerator();

// Register an in-memory vector store.
builder.Services.AddInMemoryVectorStoreRecordCollection<int, ProductVector>("products");

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMigration();

app.MapProductEndpoints();

app.UseHttpsRedirection();

app.Run();
