namespace Biblioteca.Core;

public class Emprestimo
{
    public const int PrazoEmDias = 7;

    public Livro Livro { get; }
    public Usuario Usuario { get; }
    public DateTime DataEmprestimo { get; }
    public DateTime DataPrevista => DataEmprestimo.AddDays(PrazoEmDias);
    public DateTime? DataDevolucao { get; set; }
    public bool Ativo => DataDevolucao == null;

    public Emprestimo(Livro livro, Usuario usuario, DateTime dataEmprestimo)
    {
        Livro = livro;
        Usuario = usuario;
        DataEmprestimo = dataEmprestimo;
    }
}