using FinancasPessoaisWeb.Controllers;
using FinancasPessoaisWeb.Data;
using FinancasPessoaisWeb.Models;
using FinancasPessoaisWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace FinancasPessoais.Controllers
{
    public class AccountController : Controller
    {        
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {           
            _accountService = accountService;
        }
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastro(AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                await _accountService.CadastrarConta(accountModel);
                return RedirectToAction("Login","Account");
            }
            return View(accountModel);
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> DashboardAsync()
        {
            string cpf = HttpContext.Session.GetString("UsuarioLogadoCPF");
            
            AccountModel acc = await _accountService.BuscarPorCPF(cpf);

           
            var despesasFuturas = new List<DespesaModel>
                    {
                        new DespesaModel { Nome = "Aluguel", Valor = 500.0m, DataVencimento = DateTime.Now.AddMonths(1) },
                        new DespesaModel { Nome = "Conta de Luz", Valor = 100.0m, DataVencimento = DateTime.Now.AddMonths(1).AddDays(5) }
                    };

            var viewModel = new DashboardViewModel
            {
                Account = acc,                
                DespesasFuturas = despesasFuturas
            };
            // Converte o modelo para um formato de string que pode ser passado como parâmetro na URL
            var serializedViewModel = JsonConvert.SerializeObject(viewModel);

            // Codifica a string para que possa ser usada como parâmetro na URL
            var encodedViewModel = WebUtility.UrlEncode(serializedViewModel);


            return View(viewModel);
        }
        public IActionResult Logout() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bool credenciaisValidas = await _accountService.VerificarCredenciais(loginModel.CPF, loginModel.Senha);
                if (credenciaisValidas)
                {
                    var acc = await _accountService.BuscarPorCPF(loginModel.CPF);
                    ArmazenarInformacoesNaSessao(acc);                   
                    
                    // Inclui os parâmetros diretamente na URL
                    return RedirectToAction("Dashboard", "Account");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Credenciais inválidas");
                }
            }
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizaSaldoAsync(string saldo, string id_usuario)
        {
            // Verificar se o saldo pode ser convertido em decimal
            if (decimal.TryParse(saldo, out decimal saldoDecimal))
            {                
                AccountModel usuario = await _accountService.BuscarPorId(id_usuario);
                usuario.Saldo = saldoDecimal;
                if (await _accountService.AtualizarConta(usuario))
                {
                    return Ok("Saldo atualizado com sucesso!");
                }
                else
                {
                    return BadRequest("Não foi possível atualizar o saldo!");
                }               
            }
            else
            {
                // Se a conversão falhar, retornar um erro
                return BadRequest("O valor digitado é inválido!");
            }
        }

        private void ArmazenarInformacoesNaSessao(AccountModel acc)
        {
            HttpContext.Session.SetString("UsuarioLogadoCPF", acc.CPF);
            HttpContext.Session.SetString("UsuarioLogadoTel", acc.Telefone);
            HttpContext.Session.SetString("UsuarioLogadoNome", acc.Nome);
            HttpContext.Session.SetString("UsuarioLogadoId", acc.Id.ToString());
        }

    }    
}
