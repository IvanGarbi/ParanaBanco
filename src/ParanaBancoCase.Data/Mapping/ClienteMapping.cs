using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParanaBancoCase.Business.Models;

namespace ParanaBancoCase.Data.Mapping;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR(200)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");

        builder.ToTable("Cliente");
    }
}