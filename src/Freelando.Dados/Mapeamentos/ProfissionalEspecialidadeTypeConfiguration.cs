using Freelando.Modelos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelando.Dados.Mapeamentos;
internal class ProfissionalEspecialidadeTypeConfiguration : IEntityTypeConfiguration<ProfissionalEspecialidade>
{
    public void Configure(EntityTypeBuilder<ProfissionalEspecialidade> entity)
    {
        entity.ToTable("TB_Especialidade_Profissional");
        entity.Property(e => e.ProfissionalId).HasColumnName("Id_Profissional");
        entity.Property(e => e.EspecialidadeId).HasColumnName("Id_Especialidade");
    }
}
