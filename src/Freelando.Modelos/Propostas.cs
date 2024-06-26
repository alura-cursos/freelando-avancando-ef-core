using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Modelos;

[Table("TB_Propostas")]
public class Propostas
{
 
    [Column("Id_Proposta")]
    public Guid Id { get; set; }
    [Column("Id_Profissional")]
    public Guid ProfissionalId { get; set; }
    [Column("Id_Projeto")]
    public Guid ProjetoId { get; set; }

    [Column("Valor_Proposta")]
    public decimal ValorProposta { get; set; }

    [Column("Data_Proposta")]
    public DateTime DataProposta { get; set; }

    [Column("Mensagem")]
    public string? Mensagem { get; set; }

    [Column("Prazo_Entrega")]
    public DateTime PrazoEntrega { get; set; }
}
