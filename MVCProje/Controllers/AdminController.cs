using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MVCProje.Repositories.Abstract;

namespace MVCProje.Controllers
{
    [Authorize(Roles = "Admin")]


    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IKategoriService _kategoriService;
        private readonly IOyunlarService _oyunService;
        private readonly ISepet _sepet;  
        public AdminController( UserManager<IdentityUser> userManager, IKategoriService kategoriService, IOyunlarService oyunService,ISepet sepet)
        {
            _userManager = userManager;
            _kategoriService = kategoriService;
            _oyunService = oyunService;
            _sepet= sepet;
        }

 
        

        public IActionResult Genel()
        {
            var userList = _userManager.Users.ToList();
            var userCount = _userManager.Users.Count();
            var kategoriCount = _kategoriService.GetKategoriler().Count();
            var oyunCount = _oyunService.GetOyunlar().Count();
            var sepetToplam = _sepet.Kazanc(); 

            ViewBag.UserCount = userCount;
            ViewBag.KategoriCount = kategoriCount;
            ViewBag.OyunCount = oyunCount;
            ViewBag.SepetToplam = sepetToplam;

            return View(userList);
        }

        [HttpPost]
        public async Task<IActionResult> Sil(string id)
        {
            var user = await _userManager.FindByIdAsync(id);


            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Genel");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting user.");
                return View("Genel", _userManager.Users.ToList());
            }
        }

    }
}