namespace Biblioteca.Core;

public class Biblioteca
{
    private readonly List<Emprestimo> _emprestimos = new();

    public Emprestimo Emprestar(Livro livro, Usuario usuario, DateTime data)
    {
        if (livro == null) throw new ArgumentNullException(nameof(livro));
        if (usuario == null) throw new ArgumentNullException(nameof(usuario));

        if (!livro.Disponivel)
            throw new InvalidOperationException("Livro já está emprestado.");

        var emprestimo = new Emprestimo(livro, usuario, data);
        livro.Disponivel = false;
        _emprestimos.Add(emprestimo);
        return emprestimo;
    }
}