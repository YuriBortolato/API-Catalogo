using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;

namespace umfgcloud.aplicacao.service.testes.Classes
{
    [TestClass]
    public sealed class ProdutoServicoTestes : AbstractServicoTestes
    {
        private const string C_OWNER = "Juliano Maciel";
        private const string C_CATEGORY = "produto";
        private const decimal C_VALOR_NEGATIVO = -89.90m;

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                };

                await servico.AdicionarAsync(dto);

                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();

                Assert.IsNotNull(produto);
                Assert.AreNotEqual(Guid.Empty, produto.Id);
                Assert.AreEqual("TESTE", produto.Descricao);
                Assert.AreEqual("123456789", produto.EAN);
                Assert.AreEqual(39.90m, produto.ValorCompra);
                Assert.AreEqual(89.90m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorCompraNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = -39.90m,
                    ValorVenda = 89.90m,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorVendaNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = -89.90m,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public void ProdutoServico_Instanciar_Falha()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                Assert.ThrowsException<InvalidDataException>(() => GetProdutoServicoInvalidJWT(context));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_Sucesso()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO TESTE LISTAGEM",
                    EAN = "1112223334445",
                    ValorCompra = 10.00m,
                    ValorVenda = 25.00m,
                };
                await servico.AdicionarAsync(dto);

                var produtos = await servico.ObterTodosAsync();

                Assert.IsNotNull(produtos);
                Assert.IsTrue(produtos.Any());
                Assert.AreEqual("PRODUTO TESTE LISTAGEM", produtos.First().Descricao);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_Sucesso()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                var dtoAdd = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO TESTE POR ID",
                    EAN = "5556667778889",
                    ValorCompra = 15.50m,
                    ValorVenda = 30.00m,
                };
                await servico.AdicionarAsync(dtoAdd);
                var produtoAdicionado = (await servico.ObterTodosAsync()).First();

                var produtoEncontrado = await servico.ObterPorIdAsync(produtoAdicionado.Id);

                Assert.IsNotNull(produtoEncontrado);
                Assert.AreEqual(produtoAdicionado.Id, produtoEncontrado.Id);
                Assert.AreEqual("PRODUTO TESTE POR ID", produtoEncontrado.Descricao);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_FalhaNaoEncontrado()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var idInexistente = Guid.NewGuid();

                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.ObterPorIdAsync(idInexistente));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_Sucesso()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                var dtoAdd = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO ORIGINAL",
                    EAN = "1231231231234",
                    ValorCompra = 50.00m,
                    ValorVenda = 100.00m,
                };
                await servico.AdicionarAsync(dtoAdd);
                var produtoExistente = (await servico.ObterTodosAsync()).First();

                var dtoUpdate = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = produtoExistente.Id,
                    Descricao = "PRODUTO ATUALIZADO",
                    EAN = "9879879879876",
                    ValorCompra = 60.00m,
                    ValorVenda = 120.00m,
                };

                await servico.AtualizarAsync(dtoUpdate);
                var produtoAtualizado = await servico.ObterPorIdAsync(produtoExistente.Id);

                Assert.IsNotNull(produtoAtualizado);
                Assert.AreEqual(produtoExistente.Id, produtoAtualizado.Id);
                Assert.AreEqual("PRODUTO ATUALIZADO", produtoAtualizado.Descricao);
                Assert.AreEqual("9879879879876", produtoAtualizado.EAN);
                Assert.AreEqual(60.00m, produtoAtualizado.ValorCompra);
                Assert.AreEqual(120.00m, produtoAtualizado.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_Sucesso()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                var dtoAdd = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO PARA DELETAR",
                    EAN = "0000000000000",
                    ValorCompra = 5.00m,
                    ValorVenda = 10.00m,
                };
                await servico.AdicionarAsync(dtoAdd);
                var produtoAdicionado = (await servico.ObterTodosAsync()).First();

                await servico.RemoverAsync(produtoAdicionado.Id);

                var produtosRestantes = await servico.ObterTodosAsync();
                Assert.IsFalse(produtosRestantes.Any(p => p.Id == produtoAdicionado.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}