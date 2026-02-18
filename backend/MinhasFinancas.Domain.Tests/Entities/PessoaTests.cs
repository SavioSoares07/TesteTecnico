using System;
using FluentAssertions;
using MinhasFinancas.Domain.Entities;
using Xunit;

public class PessoaTests
{
    [Fact]
    public void Deve_Calcular_Idade_Corretamente()
    {
        var pessoa = new Pessoa
        {
            DataNascimento = DateTime.Today.AddYears(-20)
        };

        var idade = pessoa.Idade;

        idade.Should().Be(20);
    }

    [Fact]
    public void Deve_Retornar_True_Quando_For_Maior_De_Idade()
    {
        var pessoa = new Pessoa
        {
            DataNascimento = DateTime.Today.AddYears(-18)
        };

        pessoa.EhMaiorDeIdade().Should().BeTrue();
    }

    [Fact]
    public void Deve_Retornar_False_Quando_For_Menor_De_Idade()
    {
        var pessoa = new Pessoa
        {
            DataNascimento = DateTime.Today.AddYears(-17)
        };

        pessoa.EhMaiorDeIdade().Should().BeFalse();
    }
}
