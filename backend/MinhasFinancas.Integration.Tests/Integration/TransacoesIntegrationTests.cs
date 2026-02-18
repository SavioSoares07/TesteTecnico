using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MinhasFinancas.API.Tests.Integration;

public class TransacoesIntegrationTests
	: IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client;

	public TransacoesIntegrationTests(WebApplicationFactory<Program> factory)
	{
		_client = factory.CreateClient();
	}

	[Fact]
	public async Task Obter_Transacoes_DeveRetornar200()
	{
		var response = await _client.GetAsync("/api/v1/transacoes");

		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task Obter_Transacao_Inexistente_DeveRetornar404()
	{
		var response = await _client.GetAsync($"/api/v1/transacoes/{Guid.NewGuid()}");

		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}

	[Fact]
	public async Task Enviar_Transacao_Invalida_DeveRetornar400()
	{
		var transacaoInvalida = new
		{
			descricao = "",
			valor = -10,
			data = DateTime.Now,
			pessoaId = Guid.NewGuid(),
			categoriaId = Guid.NewGuid()
		};

		var response = await _client.PostAsJsonAsync("/api/v1/transacoes", transacaoInvalida);

		response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
	}
}
