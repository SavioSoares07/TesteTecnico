using System;
using FluentAssertions;
using MinhasFinancas.Domain.Entities;
using Xunit;

namespace MinhasFinancas.Domain.Tests.Entities;

public class TransacaoTests
{
	[Fact]
	public void Deve_Lancar_Excecao_Quando_Menor_Tentar_Registrar_Receita()
	{
		// Arrange
		var pessoa = new Pessoa
		{
			DataNascimento = DateTime.Today.AddYears(-17)
		};

		var transacao = new Transacao
		{
			Descricao = "Salário",
			Valor = 1000,
			Tipo = Transacao.ETipo.Receita
		};

		// Act
		Action act = () => transacao.Pessoa = pessoa;

		// Assert
		act.Should()
		   .Throw<InvalidOperationException>()
		   .WithMessage("Menores de 18 anos não podem registrar receitas.");
	}

	[Fact]
	public void Deve_Permitir_Registrar_Receita_Para_Maior_De_Idade()
	{
		// Arrange
		var pessoa = new Pessoa
		{
			DataNascimento = DateTime.Today.AddYears(-20)
		};

		var transacao = new Transacao
		{
			Descricao = "Salário",
			Valor = 2000,
			Tipo = Transacao.ETipo.Receita
		};

		// Act
		transacao.Pessoa = pessoa;

		// Assert
		transacao.PessoaId.Should().Be(pessoa.Id);
	}

	[Fact]
	public void Deve_Lancar_Excecao_Quando_Tipo_For_Incompativel_Com_Categoria()
	{
		// Arrange
		var categoria = new Categoria
		{
			Finalidade = Categoria.EFinalidade.Despesa
		};

		var transacao = new Transacao
		{
			Descricao = "Supermercado",
			Valor = 300,
			Tipo = Transacao.ETipo.Receita
		};

		// Act
		Action act = () => transacao.Categoria = categoria;

		// Assert
		act.Should().Throw<InvalidOperationException>();
	}
}
