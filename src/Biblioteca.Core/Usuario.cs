
namespace Biblioteca.Core;

public class Usuario
{
    public string Nome { get; }

    public decimal MultaPendente { get; private set; }

    public Usuario(string nome)
    {
        Nome = nome;
    }

    public void RegistrarMulta(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor da multa deve ser positivo.");

        MultaPendente += valor;
    }

    public void PagarMulta()
    {
        MultaPendente = 0;
    }
}
