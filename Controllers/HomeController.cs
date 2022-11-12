using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceitaMx.Data;
using ReceitaMx.Models;
using ReceitaMx.ViewModels;

namespace ReceitaMx.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        return View(new HomeViewModel()
        {
            receitas = _context.Receitas.Include(x => x.Categoria).ToList(),
            categorias = _context.Categorias.ToList()
        });
    }

    public IActionResult Privacy()
    {
        // privacy test
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
