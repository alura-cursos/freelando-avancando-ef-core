using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Freelando.Modelo;

public class Candidatura
{
    public Candidatura()
    {
    }

    public Candidatura(Guid id, double valorProposto, string? descricaoProposta, DuracaoEmDias duracaoProposta, StatusCandidatura status, Servico servico)
    {
        Id = id;
        ValorProposto = valorProposto;
        DescricaoProposta = descricaoProposta;
        DuracaoProposta = duracaoProposta;
        Status = status;
        Servico = servico;
    }
    public Guid Id { get; set; }
    public double? ValorProposto { get; set; }
    public string? DescricaoProposta { get; set; }
    public DuracaoEmDias DuracaoProposta { get; set; }
    public StatusCandidatura Status { get; set; }
    public Guid ServicoId { get; set; }
    public Servico Servico { get; set; }
}