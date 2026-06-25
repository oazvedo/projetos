using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.domain;
using api.Domain.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace api.Domain
{
    public class Pedido
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        public PedidoStatus Status { get; set; }

        [JsonProperty("contratacao")]
        public PedidoTipoContratacaoEnum Contracacao {get;set;}

        [JsonProperty("usuario_id")]
        public Guid UsuarioId { get; set; }

        [JsonProperty("criado_em")]
        public DateTime CriadoEm {get; set;}
        
        [JsonProperty("atualizado_em")]
        public DateTime? AtualizadoEm {get; set;}

        public Pedido() { }

        public Pedido(Guid usuarioId, PedidoTipoContratacaoEnum contratacao)
        {
            Id = Guid.NewGuid();
            Status = PedidoStatus.Criado;
            Contracacao = contratacao;
            UsuarioId = usuarioId;
            CriadoEm = DateTime.UtcNow;
        }

        public void UpdateStatus(PedidoStatus novoStatus)
        {
            ValidarStatus();
            Status = novoStatus;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void UpdateContratacao(PedidoTipoContratacaoEnum novaContratacao)
        {
            ValidarStatus();
            Contracacao = novaContratacao;
            AtualizadoEm = DateTime.UtcNow;
        }

        private void ValidarStatus()
        {
            if (this.Status == PedidoStatus.Cancelado)
                throw new InvalidOperationException("Pedidos cancelados não podem ter atualização de status");
        }
    }
}