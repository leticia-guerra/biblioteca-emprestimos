namespace Biblioteca.Core;

public class Livro
{
    public string Titulo { get; }
    public bool Disponivel { get; set; } = true;

    public Livro(string titulo) => Titulo = titulo;
}