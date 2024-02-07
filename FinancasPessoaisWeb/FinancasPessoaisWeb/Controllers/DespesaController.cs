using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoaisWeb.Controllers
{
    public class DespesaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
