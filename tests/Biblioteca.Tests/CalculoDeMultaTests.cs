namespace Biblioteca.Tests;

using Biblioteca.Core;


public class CalculoDeMultaTests
{
    private static readonly DateTime Dia1 = new(2026, 11, 1);

    [Theory]
    [InlineData(8, 0.00)]  //  devolve no último dia do prazo
    [InlineData(9, 2.00)]  //  1 dia de atraso
    [InlineData(11, 6.00)]  //  3 dias de atraso
    public void T06_a_T08_Devolver_CalculaMultaPorDiaDeAtraso(int diaDaDevolucao, double multaEsperada)
    {
        var biblioteca = new Biblioteca();
        var livro = new Livro("Dom Casmurro");
        var usuario = new Usuario("Ana");
        var emprestimo = biblioteca.Emprestar(livro, usuario, Dia1);
        var dataDevolucao = new DateTime(2026, 11, diaDaDevolucao);

        decimal multa = biblioteca.Devolver(emprestimo, dataDevolucao);

        Assert.Equal((decimal)multaEsperada, multa);
        Assert.Equal((decimal)multaEsperada, usuario.MultaPendente);
    }
}