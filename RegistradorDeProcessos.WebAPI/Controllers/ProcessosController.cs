using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistradorDeProcessos.WebAPI.Contextos;
using RegistradorDeProcessos.WebAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegistradorDeProcessos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessosController : ControllerBase
    {
        private RegistradorDeProcessosContexto Contexto;

        public ProcessosController(RegistradorDeProcessosContexto contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet]
        public async Task<IEnumerable<Processo>> Get()
        {
            return await this.Contexto.Processos.Include(m => m.Movimentacoes).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Processo dadosDoProcesso)
        {
            if (!this.ValideProcesso(dadosDoProcesso))
            {
                return BadRequest();
            }

            await this.Contexto.Processos.AddAsync(dadosDoProcesso);
            await this.Contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("numeroProcesso")]
        public async Task<Processo> GetItem(string numeroProcesso)
        {
            return await this.Contexto.Processos.SingleOrDefaultAsync(x => x.Numero == numeroProcesso);
        }

        [HttpPut("numeroProcesso")]
        public async Task<ActionResult> Put(string numeroProcesso, Processo dadosDoProcesso)
        {
            var numeroDiferente = numeroProcesso != dadosDoProcesso.Numero;
            var naoExisteNoBanco = !this.Contexto.Processos.Any(x => x.Numero == numeroProcesso);

            if (naoExisteNoBanco)
            {
                return NotFound();
            }

            this.Contexto.Update(dadosDoProcesso);
            await this.Contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("numeroProcesso")]
        public async Task<ActionResult> Delete(string numeroProcesso)
        {
            if (string.IsNullOrEmpty(numeroProcesso))
            {
                return NotFound();
            }

            var processo = await this.Contexto.Processos.SingleOrDefaultAsync(x => x.Numero == numeroProcesso);

            if (processo == null)
            {
                return NotFound();
            }

            this.Contexto.Processos.Remove(processo);
            await this.Contexto.SaveChangesAsync();

            return Ok();
        }

        private bool ValideProcesso(Processo dadosDoProcesso)
        {
            return !string.IsNullOrEmpty(dadosDoProcesso.Numero) &&
                ValideNumeroFormatoCNJ(dadosDoProcesso.Numero);
        }

        private bool ValideNumeroFormatoCNJ(string numero)
        {
            const string FORMATO_NUMERO_CNJ = @"^\d{7}\-\d{2}\.\d{4}\.\d{1}\.\d{2}\.\d{4}$";

            return Regex.IsMatch(numero, FORMATO_NUMERO_CNJ);
        }
    }
}
