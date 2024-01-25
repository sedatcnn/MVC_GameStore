using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVCProje.Data;
using MVCProje.Models.Domain;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;


namespace MVCProje.Repositories.Implementation
{
    public class OyunlarService:IOyunlarService
    {
        private readonly ApplicationDbContext _context;

        public OyunlarService(ApplicationDbContext context)
        {
            _context = context;
        }


        public Oyunlar GetOyunById(Guid id)
        {
            return _context.Oyunlars
                           .Where(x => x.Id == id)
                           .FirstOrDefault();
        }
        
        public List<OyunDto> GetOyunDtos(GetOyunlar getOyunlar, string sıralama)
        {
            var oyunlarQuery = _context.Oyunlars
                .Include(x => x.OyunTurFk)
                .Include(x => x.KategoriFk)
                .Where(x => (getOyunlar.KategoriId <= 0 || getOyunlar.KategoriId == x.KategoriId)
                            && (getOyunlar.TurId <= 0 || getOyunlar.TurId == x.TurId));

            switch (sıralama)
            {
                case "Fiyat Artan":
                    oyunlarQuery = oyunlarQuery.OrderByDescending(x => x.Fiyat);
                    break;
                case "Fiyat Azalan":
                    oyunlarQuery = oyunlarQuery.OrderBy(x => x.Fiyat);
                    break;
                default:
                    oyunlarQuery = oyunlarQuery.OrderBy(x => x.OyunAdi);
                    break;
            }

            var oyunlar = oyunlarQuery.Select(x => new OyunDto
            {
                Id = x.Id,
                OyunAdi = x.OyunAdi,
                Acıklama = x.Acıklama,
                Fiyat = x.Fiyat,
                OyunImage = x.OyunImage,
                Sirket = x.Sirket,
                KategoriAdi = x.KategoriFk.KategoriAd,
                TurAdi = x.OyunTurFk.TurAd,
            }).ToList();

            return oyunlar;
        }

        public void OyunEkleGuncelle(OyunEkleGuncelleDto oyuDto)
        
        {
            if (oyuDto.Id==null)
            {
                OyunEkle(oyuDto);
            }
            else
            {
                OyunGuncelle(oyuDto);
            }
        }
        private void OyunGuncelle(OyunEkleGuncelleDto ekleGuncelleDto)
        {
            var mevcutUrun = _context.Oyunlars
                .Where(x => x.Id == ekleGuncelleDto.Id)
                .FirstOrDefault();

            if (mevcutUrun != null)
            {
                mevcutUrun.OyunAdi = ekleGuncelleDto.OyunAdi;
                mevcutUrun.Fiyat = ekleGuncelleDto.Fiyat;
                mevcutUrun.KategoriId = ekleGuncelleDto.KategoriId;
                mevcutUrun.TurId = ekleGuncelleDto.TurId;
                mevcutUrun.Sirket = ekleGuncelleDto.Sirket;

                mevcutUrun.Acıklama = ekleGuncelleDto.Aciklama;

                if (!string.IsNullOrEmpty(ekleGuncelleDto.OyunImage) && !ekleGuncelleDto.KullanMevcutResim)
                {
                    mevcutUrun.OyunImage = ekleGuncelleDto.OyunImage;
                }

                _context.Oyunlars.Update(mevcutUrun);
                _context.SaveChanges();
            }
        }

        public void Sil(Guid id)
        {
            var oyun = _context.Oyunlars.Find(id);

            if (oyun != null)
            {
                _context.Oyunlars.Remove(oyun);
                _context.SaveChanges();
            }
        }
        private void OyunEkle(OyunEkleGuncelleDto ekleGuncelleDto)
        {
            Oyunlar oyunlar = new Oyunlar()
            {
                Id = Guid.NewGuid(),
                OyunAdi = ekleGuncelleDto.OyunAdi,
                Fiyat = ekleGuncelleDto.Fiyat,
                KategoriId = ekleGuncelleDto.KategoriId,
                TurId = ekleGuncelleDto.TurId,
                Acıklama = ekleGuncelleDto.Aciklama,
                OyunImage = YukleResim(ekleGuncelleDto.ResimDosyasi),

                Sirket = ekleGuncelleDto.Sirket,
        };
      
            _context.Oyunlars.Add(oyunlar);
            _context.SaveChanges();
     
            
        }
        private string YukleResim(IFormFile resimDosyasi)
        {
            if (resimDosyasi != null && resimDosyasi.Length > 0)
            {
                var resimAdi = Path.GetFileName(resimDosyasi.FileName);
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", resimAdi);

                using (var stream = new FileStream(resimYolu, FileMode.Create))
                {
                    resimDosyasi.CopyTo(stream);
                }

                return resimAdi;
            }

            return null;
        }
        public int GetTotalOyunCount()
        {
            return _context.Oyunlars.Count();
        }
        public List<Oyunlar> GetOyunlar()
        {
            var oyunlar = _context.Oyunlars
            .OrderBy(x => x.OyunAdi)
            .ToList();

            return oyunlar;
        }
    }
}
