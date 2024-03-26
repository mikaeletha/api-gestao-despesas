using Microsoft.AspNetCore.Mvc;

namespace api_gestao_despesas.Controllers
{
    public class ExpensesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
