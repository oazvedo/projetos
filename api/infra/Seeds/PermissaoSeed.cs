using api.domain;
using Microsoft.EntityFrameworkCore;

namespace api.infra
{
    public static class PermissaoSeed
    {
        public static readonly string[] PermissionNames =
        {
            "Usuario.Read",
            "Usuario.Create",
            "Usuario.Update",
            "Usuario.Delete",
            "Usuario.EmailUpdate",
            "Usuario.PasswordUpdate",
            "Permissao.Read",
            "Permissao.Assign",
            "Permissao.Remove",
            "Permissao.RemoveAll",
            "Pedido.Read",
            "Pedido.Create",
            "Pedido.Update",
            "Pedido.Delete"
        };

        public static async Task SeedAsync(DatabaseContext context)
        {
            foreach (var permissionName in PermissionNames)
            {
                if (!await context.Permissoes.AnyAsync(p => p.Nome == permissionName))
                {
                    context.Permissoes.Add(new Permissao
                    {
                        Id = Guid.NewGuid(),
                        Nome = permissionName,
                        Descricao = $"Permissão para {permissionName}"
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
