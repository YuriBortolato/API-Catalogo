using Microsoft.AspNetCore.Mvc;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.webapi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public sealed class SigninController : ControllerBase
    {
        private readonly IUsuarioServico _servico;

        public SigninController(IUsuarioServico servico)
        {
            _servico = servico ?? throw new ArgumentNullException(nameof(servico));
        }

        /// <summary>
        /// Efetua o login do usuário
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IActionResult> 
            AutenticarAsync(UsuarioDTO.SingInRequest dto)
        {
            try
            {
                return Ok(await _servico.AutenticarAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
