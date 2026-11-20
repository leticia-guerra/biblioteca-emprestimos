
namespace Biblioteca.Core;

public class Biblioteca
{
    public const int LimiteEmprestimos = 3;
    private readonly List<Emprestimo> _emprestimos = new();

    public Emprestimo Emprestar(Livro livro, Usuario usuario, DateTime data)
    {
        if (livro == null)
            throw new ArgumentNullException(nameof(livro));

        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        if (usuario.MultaPendente > 0)
            throw new InvalidOperationException(
                "Usuário possui multa pendente e não pode realizar empréstimos.");

        if (!livro.Disponivel)
            throw new InvalidOperationException("Livro já está emprestado.");

        int ativos = _emprestimos.Count(e => e.Usuario == usuario && e.Ativo);

        if (ativos >= LimiteEmprestimos)
            throw new InvalidOperationException("Limite de empréstimos ativos atingido.");

        var emprestimo = new Emprestimo(livro, usuario, data);
        livro.Disponivel = false;
        _emprestimos.Add(emprestimo);

        return emprestimo;
    }
}
