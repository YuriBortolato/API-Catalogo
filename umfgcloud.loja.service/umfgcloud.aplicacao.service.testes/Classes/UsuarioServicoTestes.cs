namespace umfgcloud.aplicacao.service.testes.Classes;

[TestClass]
public sealed class UsuarioServicoTestes : AbstractServicoTestes
{
    #region variaveis privadas

    private const string C_CATEGORY = "cadastro e login de usuario";
    private const string C_OWNER = "Juliano Ribeiro de Souza Maciel";

    #endregion variaveis privadas

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_CadastrarAsync_Sucesso()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var siginManager = GetSignInManagerSuccess();
            var userManager = GetUserManagerSuccess();
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var dto = GetSingUpRequestDTO();

            await service.CadastrarAsync(dto);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_CadastrarAsync_Falha()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var siginManager = GetSignInManagerSuccess();
            var userManager = GetUserManagerFailed();
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var dto = GetSingUpRequestDTO();

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CadastrarAsync(dto));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    //[TestMethod]
    //[Owner(C_OWNER)]
    //[TestCategory(C_CATEGORY)]
    //public async Task UsuarioServico_CadastrarAsync_FalhaUsuarioNaoEncontrado()
    //{
    //    using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

    //    var siginManager = GetSignInManagerSuccess();
    //    var userManager = GetUserManagerWithoutFindByEmailAsync();
    //    var roleManager = GetRoleManagerExistisTrue();
    //    var unitOfWork = GetUnitOfWork(context);
    //    var service = GetAuthenticateService(siginManager, userManager, roleManager, unitOfWork);
    //    var dto = GetUserRegisterRequestDTO();

    //    await Assert.ThrowsExceptionAsync<UserNotFoundException>(() => service.RegisterUserAsync(dto));
    //}


    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_CadastrarAsync_FalhaUsuarioNaoFoiCriado()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var siginManager = GetSignInManagerSuccess();
            var userManager = GetUserManagerWithoutCreateAsync();
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var dto = GetSingUpRequestDTO();

            await Assert.ThrowsAsync<ArgumentException>(() => service.CadastrarAsync(dto));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }        
    }

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_CadastrarAsync_FalhaPapelNaoEncotrado()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var siginManager = GetSignInManagerSuccess();
            var userManager = GetUserManagerSuccess();
            var roleManager = GetRoleManagerWithoutFindByNameAsync();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var dto = GetSingUpRequestDTO();

            await Assert.ThrowsAsync<ArgumentException>(() => service.CadastrarAsync(dto));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_AutenticarAsync_Sucesso()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var identityUser = GetIdentityUser();
            var siginManager = GetSignInManagerSuccess();
            var userManager = GetUserManagerSuccess(identityUser);
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var singUp = GetSingUpRequestDTO();
            var singIn = GetSingInRequestDTO();

            await service.CadastrarAsync(singUp);
            await service.AutenticarAsync(singIn);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_AutenticarAsync_FalhaSenhaUsuarioIncorreta()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var identityUser = GetIdentityUser();
            var siginManager = GetSignInManagerFailed();
            var userManager = GetUserManagerSuccess(identityUser);
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var singIn = GetSingInRequestDTO();

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AutenticarAsync(singIn));            
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }


    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_AutenticarAsync_FalhaLockedOutUserException()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var identityUser = GetIdentityUser();
            var siginManager = GetSignInManagerLockedOut();
            var userManager = GetUserManagerSuccess(identityUser);
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var singIn = GetSingInRequestDTO();

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AutenticarAsync(singIn));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_AutenticarAsync_FalhaNotAllowedUserException()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var identityUser = GetIdentityUser();
            var siginManager = GetSignInManagerNotAllowed();
            var userManager = GetUserManagerSuccess(identityUser);
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var singIn = GetSingInRequestDTO();

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AutenticarAsync(singIn));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    [Owner(C_OWNER)]
    [TestCategory(C_CATEGORY)]
    public async Task UsuarioServico_AutenticarAsync_FalhaTwoFactorUserException()
    {
        try
        {
            using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

            var identityUser = GetIdentityUser();
            var siginManager = GetSignInManagerTwoFactorRequired();
            var userManager = GetUserManagerSuccess(identityUser);
            var roleManager = GetRoleManagerExistisTrue();
            var service = GetUsuarioServico(userManager, roleManager, siginManager);
            var singIn = GetSingInRequestDTO();

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AutenticarAsync(singIn));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}