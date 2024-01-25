using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProje.Models;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;
using MVCProje.Repositories.Implementation;

namespace MVCProje.Controllers
{

    public class KategoriController : Controller
    {
        private readonly IKategoriService _kategoriService;

        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
            var kategoriler = _kategoriService.GetKategoriler();

            return View(kategoriler);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Ekle()
        {
            if (TempData.ContainsKey("SweetAlert"))
            {
                ViewBag.SweetAlertMessage = TempData["SweetAlert"];
            }

            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Ekle(KategoriEkleGuncelleDto ekleGuncelleDto)
        {
            if (ModelState.IsValid)
            {
                _kategoriService.KategoriEkleGuncelle(ekleGuncelleDto);


                return RedirectToAction("Index");
            }

            return View(ekleGuncelleDto);

        }
        [Authorize(Roles = "Admin")]

        public IActionResult Guncelle(int id)
        {
            var kategori = _kategoriService.GetKategoribyId(id);

            var model = new KategoriEkleGuncelleDto
            {

                Id = kategori.Id,
                KategoriAd=kategori.KategoriAd
        
            };

            return View(model);
        }
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public IActionResult Sil(int id)
        {
            _kategoriService.Sil(id);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Guncelle(KategoriEkleGuncelleDto guncelleDto)
        {
            _kategoriService.KategoriEkleGuncelle(guncelleDto);

            return RedirectToAction("Index");
        }
    }
}
