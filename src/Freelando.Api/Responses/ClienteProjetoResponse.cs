namespace Freelando.Api.Responses;

public class ClienteProjetoResponse
{
    public Guid ID_Cliente { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Titulo { get; set; }
    public Guid ID_Projeto { get; set; }
    public string DS_Projeto { get; set; }
    public string Status { get; set; }
}
