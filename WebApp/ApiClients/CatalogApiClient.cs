﻿using Catalog.Models;

namespace WebApp.ApiClients;

public class CatalogApiClient(HttpClient httpClient)
{
    public async Task<List<Product>?> GetProducts()
    {
        var response = await httpClient.GetFromJsonAsync<List<Product>>($"/products");
        return response;
    }

    public async Task<Product?> GetProduct(int id)
    {
        var response = await httpClient.GetFromJsonAsync<Product>($"/products/{id}");
        return response;
    }

    public async Task<string> SupportProducts(string query)
    {
        var response = await httpClient.GetFromJsonAsync<string>($"products/support/{query}");
        return response;
    }
}