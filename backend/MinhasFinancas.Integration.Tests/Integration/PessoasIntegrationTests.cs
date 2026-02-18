using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MinhasFinancas.API.Tests.Integration;

public class PessoasIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PessoasIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Pessoa_CicloCompleto()
    {
        var novaPessoa = new
        {
            nome = "Pessoa Teste",
            dataNascimento = DateTime.Now.AddYears(-30)
        };

        // CREATE
        var postResponse = await _client.PostAsJsonAsync("/api/v1/pessoas", novaPessoa);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var location = postResponse.Headers.Location;
        location.Should().NotBeNull();

        var id = location!.Segments.Last();
        Guid.TryParse(id, out var pessoaId).Should().BeTrue();

        // GET
        var getResponse = await _client.GetAsync($"/api/v1/pessoas/{pessoaId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // DELETE
        var deleteResponse = await _client.DeleteAsync($"/api/v1/pessoas/{pessoaId}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // GET após delete
        var getAfterDelete = await _client.GetAsync($"/api/v1/pessoas/{pessoaId}");
        getAfterDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Obter_Pessoa_Inexistente_DeveRetornar404()
    {
        var response = await _client.GetAsync($"/api/v1/pessoas/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Enviar_Pessoa_Invalida_DeveRetornar400()
    {
        var pessoaInvalida = new
        {
            nome = "",
            dataNascimento = DateTime.Now
        };

        var response = await _client.PostAsJsonAsync("/api/v1/pessoas", pessoaInvalida);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
