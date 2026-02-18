using FluentAssertions;
using MinhasFinancas.Domain.Entities;
using Xunit;

public class CategoriaTests
{
    [Fact]
    public void Categoria_Despesa_Deve_Permitir_Somente_Despesa()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Despesa
        };

        categoria.PermiteTipo(Transacao.ETipo.Despesa).Should().BeTrue();
        categoria.PermiteTipo(Transacao.ETipo.Receita).Should().BeFalse();
    }

    [Fact]
    public void Categoria_Receita_Deve_Permitir_Somente_Receita()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Receita
        };

        categoria.PermiteTipo(Transacao.ETipo.Receita).Should().BeTrue();
        categoria.PermiteTipo(Transacao.ETipo.Despesa).Should().BeFalse();
    }

    [Fact]
    public void Categoria_Ambas_Deve_Permitir_Todos_Tipos()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Ambas
        };

        categoria.PermiteTipo(Transacao.ETipo.Receita).Should().BeTrue();
        categoria.PermiteTipo(Transacao.ETipo.Despesa).Should().BeTrue();
    }
}
