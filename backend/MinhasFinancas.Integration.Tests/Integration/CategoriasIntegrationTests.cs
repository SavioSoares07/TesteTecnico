using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MinhasFinancas.API.Tests.Integration;

public class CategoriasIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CategoriasIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Categorias_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/v1/categorias");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Obter_Categorias_DeveRetornarEstruturaPaginada()
    {
        var response = await _client.GetAsync("/api/v1/categorias?page=1&pageSize=5");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().Contain("items");
        content.Should().Contain("totalCount");
    }

    [Fact]
    public async Task Obter_Categorias_DeveRespeitarPageSize()
    {
        var response = await _client.GetAsync("/api/v1/categorias?page=1&pageSize=2");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        json.ValueKind.Should().Be(JsonValueKind.Object);

        var items = json.GetProperty("items");

        items.ValueKind.Should().Be(JsonValueKind.Array);
        items.GetArrayLength().Should().BeLessThanOrEqualTo(2);
    }

    [Fact]
    public async Task Obter_Categorias_ComSearch_DeveFiltrarResultados()
    {
        var response = await _client.GetAsync("/api/v1/categorias?search=Ali");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Ali");
    }

    [Fact]
    public async Task Obter_Categoria_PorId_Inexistente_DeveRetornar404()
    {
        var id = Guid.NewGuid();

        var response = await _client.GetAsync($"/api/v1/categorias/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Enviar_Categoria_Valida_DeveRetornar201_E_Location()
    {
        var novaCategoria = new
        {
            descricao = "Teste",
            finalidade = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v1/categorias", novaCategoria);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        response.Headers.Location.Should().NotBeNull();

        var location = response.Headers.Location!;

        location.AbsolutePath.Should().StartWith("/api/v1/Categorias/");

        var segments = location.Segments;
        Guid.TryParse(segments.Last(), out var id).Should().BeTrue();
    }
}
