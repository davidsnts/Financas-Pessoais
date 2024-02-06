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
        public IActionResult Dashboard(string serializedViewModel)
        {
            // Decodifica a string que foi passada como parâmetro na URL
            var decodedViewModel = WebUtility.UrlDecode(serializedViewModel);

            // Desserializa o modelo
            var viewModel = JsonConvert.DeserializeObject<DashboardViewModel>(decodedViewModel);

            return View(viewModel);
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

                    
                    var saldoConta = 1000.0m;
                    var despesasFuturas = new List<DespesaModel>
                    {
                        new DespesaModel { Nome = "Aluguel", Valor = 500.0m, DataVencimento = DateTime.Now.AddMonths(1) },
                        new DespesaModel { Nome = "Conta de Luz", Valor = 100.0m, DataVencimento = DateTime.Now.AddMonths(1).AddDays(5) }                        
                    };

                    var viewModel = new DashboardViewModel
                    {
                        Account = acc,
                        SaldoConta = saldoConta,
                        DespesasFuturas = despesasFuturas
                    };
                    // Converte o modelo para um formato de string que pode ser passado como parâmetro na URL
                    var serializedViewModel = JsonConvert.SerializeObject(viewModel);

                    // Codifica a string para que possa ser usada como parâmetro na URL
                    var encodedViewModel = WebUtility.UrlEncode(serializedViewModel);

                    // Inclui os parâmetros diretamente na URL
                    return RedirectToAction("Dashboard", "Account", new { serializedViewModel = encodedViewModel });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Credenciais inválidas");
                }
            }
            return View(loginModel);
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
