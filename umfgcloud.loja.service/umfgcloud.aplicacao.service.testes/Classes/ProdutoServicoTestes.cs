using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;

namespace umfgcloud.aplicacao.service.testes.Classes;

[TestClass]
public sealed class ProdutoServicoTestes : AbstractServicoTestes
{
    #region variaveis privadas

    private const string C_CATEGORY = "produto";
    private const string C_OWNER = "Juliano Ribeiro de Souza Maciel";

    #endregion variaveis privadas

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task ProdutoServico_AdicionarAsync_Sucesso()
    {
        try
        {
            //obtem instacia do contexto do banco de dados
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var repositorio = GetProdutoRepositorio(context);
            var service = GetProdutoServico(repositorio);
            var dto = new ProdutoDTO.ProdutoRequest()
            {
                Descricao = "Produto Teste",
                EAN = "123456789",
                ValorCompra = 10.00m,
                ValorVenda = 15.00m,
            };

            await service.AdicionarAsync(dto);

            var produto = (await repositorio.ObterTodosAsync()).FirstOrDefault();

            Assert.IsNotNull(produto);
            Assert.IsFalse(Guid.Empty.Equals(produto.Id));
            Assert.AreEqual(UserId, produto.CreatedByUserId);
            Assert.AreEqual(UserId, produto.UpdatedByUserId);
            Assert.AreEqual(UserEmail, produto.CreatedByUserEmail);
            Assert.AreEqual(UserEmail, produto.UpdatedByUserEmail);
            Assert.IsTrue(produto.CreatedAt <= DateTime.UtcNow);
            Assert.IsTrue(produto.UpdatedAt <= DateTime.UtcNow);
            Assert.AreEqual(dto.Descricao, produto.Descricao);
            Assert.AreEqual(dto.EAN, produto.EAN);
            Assert.AreEqual(dto.ValorCompra, produto.ValorCompra);
            Assert.AreEqual(dto.ValorVenda, produto.ValorVenda);
            Assert.IsTrue(produto.IsActive);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}
