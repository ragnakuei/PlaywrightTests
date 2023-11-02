using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers;

public class TodoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}