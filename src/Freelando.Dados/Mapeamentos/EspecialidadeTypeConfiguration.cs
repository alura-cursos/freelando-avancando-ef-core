using Freelando.Modelo;
using Freelando.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Dados.Mapeamentos;
internal class EspecialidadeTypeConfiguration : IEntityTypeConfiguration<Especialidade>
{
    public void Configure(EntityTypeBuilder<Especialidade> entity)
    {
        entity.ToTable("TB_Especialidades");
        entity.Property(e => e.Id).HasColumnName("ID_Especialidade");
        entity.Property(e => e.Descricao).HasColumnName("DS_Especialidade");
    }
}