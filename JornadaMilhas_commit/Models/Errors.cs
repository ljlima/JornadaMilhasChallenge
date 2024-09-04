namespace JornadaMilhas.Models;

public class Errors
{
    public List<string> ErrorMessages { get; set; }
    public Errors()
    {
        ErrorMessages.Add("Nenhum destino foi encontrado");
    }
    public List<string> RetornaMensagem()
    {
        return ErrorMessages;
    }
}
