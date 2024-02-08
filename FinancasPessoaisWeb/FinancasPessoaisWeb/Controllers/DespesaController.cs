using FinancasPessoaisWeb.Models;
using FinancasPessoaisWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace FinancasPessoaisWeb.Controllers
{
    public class DespesaController : Controller
    {
        private readonly AccountService _accountService;
        private readonly DespesaService _despesaService;
        public DespesaController(AccountService accountService, DespesaService despesaService)
        {
            _accountService = accountService;
            _despesaService = despesaService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string cpf = HttpContext.Session.GetString("UsuarioLogadoCPF");

            AccountModel acc = await _accountService.BuscarPorCPF(cpf);

            var despesasFuturas = await _despesaService.BuscarPorIdAccount(acc.Id.ToString());

            var viewModel = new DashboardViewModel
            {
                Account = acc,
                DespesasFuturas = despesasFuturas
            };           

            return View(viewModel);            
        }
        [HttpGet]
        public async Task<IActionResult> GetDespesa(int id)
        {
            var despesa = await _despesaService.GetDespesaById(id);
            if (despesa == null)
            {
                return NotFound(); 
            }
            return PartialView("_EditDespesaPartial", despesa); 
        }
        [HttpPost]
        public async Task<IActionResult> EditDespesa(DespesaModel despesa)
        {            
            if (despesa == null)
            {
                return NotFound();
            }
            await _despesaService.AtualizarDespesa(despesa);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Apagar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _despesaService.Apagar(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AddDespesa(DespesaModel despesa)
        {
            // Verificar se o saldo pode ser convertido em decimal
            if (decimal.TryParse(despesa.Valor.ToString(), out decimal valorDecimal))
            {
                despesa.Valor = valorDecimal;
                if (await _despesaService.CadastrarDespesa(despesa))
                {
                    return Ok("Despesa adicionada com sucesso!");
                }
                else
                {
                    return BadRequest("Não foi possível adicionar a Despesa!");
                }
            }
            else
            {               
                return BadRequest("O valor da despesa é inválido!");
            }
        }
    }
}
