namespace Biblioteca.Core;


public static class CalculadoraDeMulta
{
    public const decimal ValorPorDiaDeAtraso = 2.00m;

   
    public static decimal Calcular(DateTime dataPrevista, DateTime dataDevolucao)
    {
        int diasDeAtraso = (dataDevolucao.Date - dataPrevista.Date).Days;

        return diasDeAtraso > 0
            ? diasDeAtraso * ValorPorDiaDeAtraso
            : 0m;
    }
}