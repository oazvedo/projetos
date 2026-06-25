using api.domain;
using api.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.infra
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<UsuarioPermissao> UsuarioPermissoes { get; set; }
        public DbSet<Pedido> Pedidos {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(u => u.Nome)
                    .HasColumnName("nome")
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(u => u.Status)
                    .HasColumnName("status")
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .HasColumnName("password_hash")
                    .IsRequired();

                entity.Property(u => u.CriadoEm)
                    .HasColumnName("criado_em")
                    .IsRequired();

                entity.Property(u => u.AtualizadoEm)
                    .HasColumnName("atualizado_em")
                    .IsRequired(false);
            });

            modelBuilder.Entity<Permissao>(entity =>
            {
                entity.ToTable("permissoes");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(p => p.Nome)
                    .HasColumnName("nome")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Descricao)
                    .HasColumnName("descricao")
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<UsuarioPermissao>(entity =>
            {
                entity.ToTable("usuario_permissoes");

                entity.HasKey(up => new { up.UsuarioId, up.PermissaoId });

                entity.HasOne(up => up.Usuario)
                    .WithMany(u => u.UsuarioPermissoes)
                    .HasForeignKey(up => up.UsuarioId);

                entity.HasOne(up => up.Permissao)
                    .WithMany(p => p.UsuarioPermissoes)
                    .HasForeignKey(up => up.PermissaoId);
            });
        
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("pedidos");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id")
                    .IsRequired();
                
                entity.Property(u => u.Status)
                    .HasColumnName("status");

                entity.Property(u => u.Contracacao)
                    .HasColumnName("contratacao");
                
                entity.Property(u => u.UsuarioId)
                    .HasColumnName("usuario_id");

                entity.Property(u => u.CriadoEm)
                    .HasColumnName("criado_em")
                    .IsRequired();

                entity.Property(u => u.AtualizadoEm)
                    .HasColumnName("atualizado_em")
                    .IsRequired(false);
            });
        }
    }
}
