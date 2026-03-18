using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes
{
    public sealed class UsuarioServico : IUsuarioServico
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsuarioServico(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task<UsuarioDTO.SingInResponse> AutenticarAsync(UsuarioDTO.SingInRequest dto)
        {
            throw new NotImplementedException();
        }

        public async Task CadastrarAsync(UsuarioDTO.SingUpRequest dto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true,
            };

            var resultado = await _userManager.CreateAsync(identityUser, dto.Password);

            if (!resultado.Succeeded && resultado.Errors.Any())
                throw new InvalidOperationException(string.Join("\n", resultado.Errors.Select(x => x.Description)));

            await _userManager.SetLockoutEnabledAsync(identityUser, true);
        }
    }
}
