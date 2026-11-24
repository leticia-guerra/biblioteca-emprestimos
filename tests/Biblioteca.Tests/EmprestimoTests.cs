using Xunit;
using Biblioteca.Core;
using Servico = Biblioteca.Core.Biblioteca;

namespace Biblioteca.Tests;

public class EmprestimoTests
{
    private static readonly DateTime Dia1 = new(2026, 11, 1);

    [Fact]
    public void T01_Emprestar_LivroDisponivel_CriaEmprestimoEMarcaIndisponivel()
    {
        var servico = new Servico();
        var livro = new Livro("Dom Casmurro");
        var usuario = new Usuario("Ana");

        var emprestimo = servico.Emprestar(livro, usuario, Dia1);

        Assert.False(livro.Disponivel);
        Assert.Equal(Dia1, emprestimo.DataEmprestimo);
        Assert.Equal(Dia1.AddDays(7), emprestimo.DataPrevista);
    }

    [Fact]
    public void T02_Emprestar_LivroJaEmprestado_LancaErroEMantemIndisponivel()
    {
        var servico = new Servico();
        var livro = new Livro("Dom Casmurro");
        livro.Disponivel = false;

        var erro = Assert.Throws<InvalidOperationException>(
            () => servico.Emprestar(livro, new Usuario("Ana"), Dia1));

        Assert.Contains("emprestado", erro.Message);
        Assert.False(livro.Disponivel);
    }

    [Fact]
    public void T03a_Emprestar_LivroNulo_LancaArgumentNullException()
    {
        var servico = new Servico();

        Assert.Throws<ArgumentNullException>(
            () => servico.Emprestar(null!, new Usuario("Ana"), Dia1));
    }

    [Fact]
    public void T03b_Emprestar_UsuarioNulo_LancaArgumentNullException()
    {
        var servico = new Servico();

        Assert.Throws<ArgumentNullException>(
            () => servico.Emprestar(new Livro("Dom Casmurro"), null!, Dia1));
    }
}