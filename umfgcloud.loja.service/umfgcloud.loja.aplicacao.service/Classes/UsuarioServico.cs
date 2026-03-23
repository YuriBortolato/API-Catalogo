using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.Classes;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes
{
    public sealed class UsuarioServico : IUsuarioServico
    {
        private readonly string[] _roles = [Role.Desenvolvedor, Role.Admin, Role.Padrao];

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioServico(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
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

            foreach (var role in _roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if ((await _roleManager.FindByNameAsync(Role.Padrao)) is null)
                throw new ArgumentException("Configuração padrão não cadastrada!");

            await _userManager.AddToRoleAsync(identityUser, Role.Padrao);
            await _userManager.SetLockoutEnabledAsync(identityUser, true);
        }
    }
}
