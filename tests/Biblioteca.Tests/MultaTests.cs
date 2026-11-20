
using Xunit;
using Biblioteca.Core;
using Servico = Biblioteca.Core.Biblioteca;

namespace Biblioteca.Tests;

public class MultaTests
{
    private static readonly DateTime Dia1 = new(2026, 11, 1);

    [Fact]
    public void T11_UsuarioComMulta_NaoPodeEmprestar()
    {
        var servico = new Servico();
        var usuario = new Usuario("Ana");
        var livro = new Livro("Dom Casmurro");

        usuario.RegistrarMulta(6m);

        var erro = Assert.Throws<InvalidOperationException>(
            () => servico.Emprestar(livro, usuario, Dia1));

        Assert.Contains("multa pendente", erro.Message);
        Assert.True(livro.Disponivel);
    }

    [Fact]
    public void T12_UsuarioPagaMulta_PodeEmprestar()
    {
        var servico = new Servico();
        var usuario = new Usuario("Ana");
        var livro = new Livro("Dom Casmurro");

        usuario.RegistrarMulta(6m);
        usuario.PagarMulta();

        Assert.Equal(0m, usuario.MultaPendente);

        var emprestimo = servico.Emprestar(livro, usuario, Dia1);

        Assert.NotNull(emprestimo);
        Assert.False(livro.Disponivel);
    }
}
