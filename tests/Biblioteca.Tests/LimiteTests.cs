using Xunit;
using Biblioteca.Core;
using Servico = Biblioteca.Core.Biblioteca;

namespace Biblioteca.Tests;

public class LimiteTests
{
    private static readonly DateTime Dia1 = new(2026, 11, 1);

    [Fact]
    public void T09_Emprestar_TerceiroLivro_DentroDoLimite_Funciona()
    {
        var servico = new Servico();
        var usuario = new Usuario("Ana");
        servico.Emprestar(new Livro("L1"), usuario, Dia1);
        servico.Emprestar(new Livro("L2"), usuario, Dia1);
        var terceiro = new Livro("L3");

        var emprestimo = servico.Emprestar(terceiro, usuario, Dia1);

        Assert.NotNull(emprestimo);
        Assert.False(terceiro.Disponivel);
    }

    [Fact]
    public void T10_Emprestar_QuartoLivro_AcimaDoLimite_Falha()
    {
        var servico = new Servico();
        var usuario = new Usuario("Ana");
        servico.Emprestar(new Livro("L1"), usuario, Dia1);
        servico.Emprestar(new Livro("L2"), usuario, Dia1);
        servico.Emprestar(new Livro("L3"), usuario, Dia1);
        var quarto = new Livro("L4");

        var erro = Assert.Throws<InvalidOperationException>(
            () => servico.Emprestar(quarto, usuario, Dia1));

        Assert.Contains("Limite", erro.Message);
        Assert.True(quarto.Disponivel);
    }
}