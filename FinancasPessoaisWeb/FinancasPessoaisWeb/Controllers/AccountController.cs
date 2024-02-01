using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
