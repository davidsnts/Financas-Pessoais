using FinancasPessoaisWeb.Controllers;
using FinancasPessoaisWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {                
                return RedirectToAction("Index","Home");
            }
            return View(accountModel);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(loginModel);
        }
    }
}
