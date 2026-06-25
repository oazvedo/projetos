using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.application.dtos.usuario;
using api.application.services.interfaces;
using api.Application.DTOs.Usuario;
using api.domain;
using api.domain.interfaces;

namespace api.application.services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsuarioDto> CreateAsync(Usuario usuario)
        {
            await _repository.CreateAsync(usuario);
            return await Task.FromResult(new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CriadoEm = usuario.CriadoEm,
                AtualizadoEm = usuario.AtualizadoEm,
                Status = usuario.Status
            });
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return await Task.FromResult(true);}

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                CriadoEm = u.CriadoEm,
                AtualizadoEm = u.AtualizadoEm,
                Status = u.Status
            });
        }

        public async Task<UsuarioDto?> GetByEmailAsync(string email)
        {
            var usuario = await _repository.GetByEmailAsync(email);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CriadoEm = usuario.CriadoEm,
                AtualizadoEm = usuario.AtualizadoEm,
                Status = usuario.Status
            };
        }

        public async Task<UsuarioDto?> GetByIdAsync(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CriadoEm = usuario.CriadoEm,
                AtualizadoEm = usuario.AtualizadoEm,
                Status = usuario.Status
            };
        }

        public async Task<UsuarioDto?> UpdateAsync(Guid id, UpdateUsuarioRequest request)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;

            usuario.Nome = request.Nome;
            usuario.Email = request.Email;
            usuario.AtualizadoEm = DateTime.UtcNow;

            await _repository.UpdateAsync(usuario);
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CriadoEm = usuario.CriadoEm,
                AtualizadoEm = usuario.AtualizadoEm,
                Status = usuario.Status
            };
        }

        public async Task<UsuarioDto?> UpdateEmail(Guid Id, string email)
        {
            var usuario = await _repository.GetByIdAsync(Id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            usuario.Email = email;
            await _repository.UpdateAsync(usuario);
            return await Task.FromResult(new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CriadoEm = usuario.CriadoEm,
                AtualizadoEm = usuario.AtualizadoEm,
                Status = usuario.Status
            });
        }

        public async Task<UsuarioDto?> UpdateEmailAsync(Guid id, string email)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            usuario.Email = email;
            await _repository.UpdateAsync(usuario);
            return await Task.FromResult(new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CriadoEm = usuario.CriadoEm,
                AtualizadoEm = usuario.AtualizadoEm,
                Status = usuario.Status
            });
        }
    
        public Task<bool> UpdatePasswordAsync(Guid id, string password)
            => _repository.UpdatePasswordAsync(id, password);
    }
}