using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;

public class OyunlarController : Controller
{
    private readonly IOyunlarService _oyunService;
    private readonly IKategoriService _kategoriService;
    private readonly ITurService _turService;

    public OyunlarController(IOyunlarService oyunService, IKategoriService kategoriService, ITurService turService)
    {
        _oyunService = oyunService;
        _kategoriService = kategoriService;
        _turService = turService;
    }

    public IActionResult Index(string sıralama)
    {
        var oyunlar = _oyunService.GetOyunDtos(new GetOyunlar { KategoriId = -1, TurId = -1 }, sıralama);
        return View(oyunlar);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Ekle()
    {
        var model = new OyunEkleGuncelleViewModel
        {
            Kategoriler = _kategoriService.GetKategoriler(),
            OyunTurler = _turService.GetTurs(),
            Oyun = new OyunEkleGuncelleDto()
        };

        return View(model);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Ekle(OyunEkleGuncelleViewModel input)
    {
        
         _oyunService.OyunEkleGuncelle(input.Oyun);
         return RedirectToAction("Index");
    
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Guncelle(Guid id)
    {
        var oyun = _oyunService.GetOyunById(id);

        var oyunDto = new OyunEkleGuncelleDto
        {
            Id = oyun.Id,
            OyunAdi = oyun.OyunAdi,
            Fiyat = oyun.Fiyat,
            KategoriId = oyun.KategoriId,
            TurId = oyun.TurId,
            Aciklama = oyun.Acıklama,
            OyunImage = oyun.OyunImage,
            Sirket = oyun.Sirket,
        };

        var model = new OyunEkleGuncelleViewModel
        {
            Kategoriler = _kategoriService.GetKategoriler(),
            OyunTurler = _turService.GetTurs(),
            Oyun = oyunDto
        };

        return View(model);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Sil")]
    [ValidateAntiForgeryToken]
    public IActionResult Sil(Guid id)
    {
        _oyunService.Sil(id);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Guncelle(OyunEkleGuncelleViewModel viewModel)
    {
        
            _oyunService.OyunEkleGuncelle(viewModel.Oyun);
            return RedirectToAction("Index");
        

      
    }
}
