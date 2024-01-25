using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCProje.Data;
using MVCProje.Models;
using MVCProje.Models.Domain;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;
using System.Drawing.Printing;


namespace MVCProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOyunlarService _oyunService;
        private readonly IKategoriService _kategoriService;
        private readonly ITurService _turService;
        private readonly ISepet _sepetService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IOyunlarService oyunService, IKategoriService kategoriService, ITurService turService,ISepet sepet, ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _oyunService = oyunService;
            _kategoriService = kategoriService;
            _turService = turService;
            _sepetService = sepet;
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(int turId = -1, int kategoriId = -1, string sortOrder = "", int page = 1, int pageSize = 9)
        {
            var turler = _turService.GetTurs();
            turler.Insert(0, new Tur { Id = -1, TurAd = "Tümü" });

            var kategoriler = _kategoriService.GetKategoriler(); 
            kategoriler.Insert(0, new Kategori { Id = -1, KategoriAd = "Tümü" });

            var model = new HomeIndexViewModel()
            {
                SayfaSayýsý = page,
                SayfaBoyutu = pageSize,
                ToplamSayfa = (int)Math.Ceiling((double)_oyunService.GetTotalOyunCount() / pageSize),
                TurId = turId,
                KategoriId = kategoriId,
                Turler = turler,
                Kategoriler = kategoriler,
                Oyunlar = _oyunService.GetOyunDtos(new Models.DTO.GetOyunlar
                {
                    KategoriId = kategoriId,
                    TurId = turId
                }, sortOrder)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
            };

            model.Sýralama = sortOrder;

            return View(model);
        }

        public IActionResult OneCikanlar(int turId = -1, int kategoriId = -1, string sortOrder = "")
        {
            var turler = _turService.GetTurs();
            turler.Insert(0, new Tur { Id = -1, TurAd = "Tümü" });

            var kategoriler = _kategoriService.GetKategoriler();
            kategoriler.Insert(0, new Kategori { Id = -1, KategoriAd = "Tümü" });

            var allGames = _oyunService.GetOyunDtos(new Models.DTO.GetOyunlar
            {
                KategoriId = kategoriId,
                TurId = turId
            }, sortOrder).ToList();

            var random = new Random();
            var shuffledGames = allGames.OrderBy(x => random.Next()).Take(15).ToList();

            var model = new HomeIndexViewModel()
            {
                TurId = turId,
                KategoriId = kategoriId,
                Turler = turler,
                Kategoriler = kategoriler,
                Oyunlar = shuffledGames
            };

            model.Sýralama = sortOrder;

            return View("OneCikanlar", model);
        }

        [HttpGet]
        public IActionResult IsAuthenticated()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            return Json(new { isAuthenticated });
        }

        [HttpPost]
        [Authorize(Roles = "Kullanici")]
        public IActionResult Sepet(Guid oyunId)
        {
            var oyun = _oyunService.GetOyunById(oyunId);

            if (oyun != null)
            {
                var sepetOyun = new Sepet
                {
                    OyunId = oyun.Id,
                    OyunAdi = oyun.OyunAdi,
                    Fiyat = oyun.Fiyat,
                    OyunImage = oyun.OyunImage
                };

                try
                {
                    _sepetService.SepetEkleGuncelle(sepetOyun);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
           
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Kullanici")]
        public IActionResult IsInCart(Guid oyunId)
        {
            var isInCart = _sepetService.IsInCart(oyunId);
            return Json(new { isInCart });
        }

        public IActionResult Sepet()
        {
            var cartItems = _sepetService.GetSepets(); 

            var model = new ShoppingCartItemViewModel
            {
                Getir = cartItems
            };

            return View(model);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]

        public IActionResult Sil(Guid id)
        {
            _sepetService.Sil(id);

            return RedirectToAction("Sepet");
        }
        [HttpPost]
        public IActionResult Tamamlandý()
        {
            decimal totalAmount = _sepetService.GetTotalAmount();
            _sepetService.SaveOrderDetails(totalAmount); 
            _sepetService.ClearSepet();
            return View();
        }




    }
}
