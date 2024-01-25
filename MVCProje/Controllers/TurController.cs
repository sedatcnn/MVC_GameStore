// TurController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;

public class TurController : Controller
{
    private readonly ITurService _turService;

    public TurController(ITurService turService)
    {
        _turService = turService;
    }
    [Authorize(Roles = "Admin")]

    public IActionResult Index()
    {
        var turlar = _turService.GetTurs();
        return View(turlar);
    }
    [Authorize(Roles = "Admin")]

    public IActionResult Ekle()
    {
        // Check for SweetAlert message in TempData
        if (TempData.ContainsKey("SweetAlert"))
        {
            ViewBag.SweetAlertMessage = TempData["SweetAlert"];
        }

        return View();
    }
    [Authorize(Roles = "Admin")]


    [HttpPost]
    public IActionResult Ekle(TurEkleGuncelleDto input)
    {
        if (ModelState.IsValid)
        {
            _turService.TurEkleGuncell(input);

            // SweetAlert mesajını TempData'ye ekle
            TempData["SweetAlert"] = "Tur Ekleme İşlemi Başarılı";

            return RedirectToAction("Index");
        }

        return View(input);
    }

    [Authorize(Roles = "Admin")]

    public IActionResult Guncelle(int id)
    {
        var tur = _turService.GetTurNyId(id);

        var model = new TurEkleGuncelleDto
        {
            Id = tur.Id,
            Ad = tur.TurAd
        };

        return View(model);
    }
    [Authorize(Roles = "Admin")]

    [HttpPost, ActionName("Sil")]
    [ValidateAntiForgeryToken]
    public IActionResult Sil(int id)
    {
        _turService.Sil(id);


        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]

    [HttpPost]
    public IActionResult Guncelle(TurEkleGuncelleDto input)
    {
        
            _turService.TurEkleGuncell(input);



        return RedirectToAction("Index");
    }
}
