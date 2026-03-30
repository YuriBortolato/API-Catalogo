using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Entidades;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes
{
    public sealed class ProdutoServico : AbstractServico, IProdutoServico
    {
        public ProdutoServico(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task AdicionarAsync(ProdutoDTO.ProdutoRequest dto)
        {
            var produto = new ProdutoEntity(UserId, UserEmail);
        }

        public Task AtualizarAsync(ProdutoDTO.AbstractProdutoWithIdDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<ProdutoDTO.ProdutoResponse> ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProdutoDTO.ProdutoResponse>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
