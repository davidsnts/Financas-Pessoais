using FinancasPessoaisWeb.Controllers;
using FinancasPessoaisWeb.Data;
using FinancasPessoaisWeb.Models;
using FinancasPessoaisWeb.Services;
using Microsoft.AspNetCore.Mvc;

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
                    return RedirectToAction("Index", "Home");
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
