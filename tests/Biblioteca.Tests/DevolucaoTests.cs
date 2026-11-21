namespace Biblioteca.Tests;

using Biblioteca.Core;

public class DevolucaoTests
{
    private static readonly DateTime Dia1 = new(2026, 11, 1);

    [Fact]
    public void T04_Devolver_EmprestimoAtivo_LivroFicaDisponivelEDataDeDevolucaoRegistrada()
    {
        
        var biblioteca = new Biblioteca();
        var livro = new Livro("Dom Casmurro");
        var usuario = new Usuario("Ana");
        var emprestimo = biblioteca.Emprestar(livro, usuario, Dia1);
        var dia5 = new DateTime(2026, 11, 5);

        biblioteca.Devolver(emprestimo, dia5);

        Assert.True(livro.Disponivel);
        Assert.Equal(dia5, emprestimo.DataDevolucao);
        Assert.False(emprestimo.Ativo);
    }

    [Fact]
    public void T05_Devolver_EmprestimoJaDevolvido_LancaInvalidOperationException()
    {
        var biblioteca = new Biblioteca();
        var livro = new Livro("Dom Casmurro");
        var usuario = new Usuario("Ana");
        var emprestimo = biblioteca.Emprestar(livro, usuario, Dia1);
        var primeiraDevolucao = new DateTime(2026, 11, 5);
        biblioteca.Devolver(emprestimo, primeiraDevolucao);

        var erro = Assert.Throws<InvalidOperationException>(
            () => biblioteca.Devolver(emprestimo, new DateTime(2026, 11, 6)));

        Assert.Equal("Este empréstimo já foi devolvido.", erro.Message);
        Assert.Equal(primeiraDevolucao, emprestimo.DataDevolucao);
    }
}