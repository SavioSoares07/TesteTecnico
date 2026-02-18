using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MinhasFinancas.API.Tests.Integration;

public class TotaisIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TotaisIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Obter_Totais_PorPessoa_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/v1/totais/pessoas");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Obter_Totais_PorPessoa_ComPaginacao_DeveFuncionar()
    {
        var response = await _client.GetAsync("/api/v1/totais/pessoas?page=1&pageSize=5");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        json.ValueKind.Should().Be(JsonValueKind.Object);
        json.GetProperty("items").ValueKind.Should().Be(JsonValueKind.Array);
    }

    [Fact]
    public async Task Obter_Totais_PorCategoria_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/v1/totais/categorias");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Obter_Totais_PorCategoria_ComPaginacao_DeveFuncionar()
    {
        var response = await _client.GetAsync("/api/v1/totais/categorias?page=1&pageSize=5");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        json.ValueKind.Should().Be(JsonValueKind.Object);
        json.GetProperty("items").ValueKind.Should().Be(JsonValueKind.Array);
    }
}
