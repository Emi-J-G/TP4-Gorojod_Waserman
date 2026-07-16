using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP04_Gorojod_Waserman.Models;

namespace TP04_Gorojod_Waserman.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        BD bd = new BD();
        Album album = new Album();
        ViewBag.cFig = bd.getCantTotal();
        ViewBag.figus = bd.getFiguritas();
        ViewBag.peg = album.getPegadas();
        return View();
    }

    public IActionResult abrirSobre() {
        BD bd = new BD();
        List<Figuritas> figuritas = bd.abrirSobre();
        ViewBag.infoFig = figuritas;
        return View("confirmarSobre");
    } //Aca va a devolver 5 figuritas que se deberan mostrar en la view

    [HttpPost]
    public IActionResult confirmarSobre(List<Figuritas> figuritas, bool seGuarda){
        BD bd = new BD();
        if (seGuarda){
            foreach(Figuritas figurita in figuritas){
                bd.sumarFiguritas(figurita.id);
            }
        }
        return RedirectToAction("Index");
    } //Si eligen quedarse con las figuritas despues de abrir el sobre, se ejecutara esto y se añadiran en la base

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
