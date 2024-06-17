using Freelando.Modelo;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelando.Modelos;

namespace Freelando.Dados.Mapeamentos;
internal class ProfissionalTypeConfiguration : IEntityTypeConfiguration<Profissional>
{
    public void Configure(EntityTypeBuilder<Profissional> entity)
    {
        entity.ToTable("TB_Profissionais");

        entity.Property(e => e.Id).HasColumnName("Id_Profissional");

        entity
            .HasMany(e => e.Especialidades)
            .WithMany(e => e.Profissionais)
            .UsingEntity<ProfissionalEspecialidade>(
            l => l.HasOne<Especialidade>(e => e.Especialidade).WithMany(e => e.ProfissionaisEspecialidade).HasForeignKey(e => e.EspecialidadeId),
            r => r.HasOne<Profissional>(e => e.Profissional).WithMany(e => e.ProfissionaisEspecialidade).HasForeignKey(e => e.ProfissionalId)
            );
    }
}
