using MVCProje.Data;
using MVCProje.Models.Domain;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;

namespace MVCProje.Repositories.Implementation
{
    public class KategoriService : IKategoriService
    {

        private readonly ApplicationDbContext _context;

        public KategoriService(ApplicationDbContext context)
        {
             _context = context;
        }
        public Kategori GetKategoribyId(int id)
        {
            return _context.Kategoris
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public List<Kategori> GetKategoriler()
        {
            var kategoriler = _context.Kategoris
            .OrderBy(x => x.KategoriAd)
            .ToList();

            return kategoriler;
        }

        public void KategoriEkleGuncelle(KategoriEkleGuncelleDto guncelle)
        {
            if (!guncelle.Id.HasValue)
            {
                KategoriEkle(guncelle);
            }
            else
            {
                KategoriGuncelle(guncelle);
            }
        }
        private void KategoriGuncelle(KategoriEkleGuncelleDto input)
        {
            var mevcutKategori = _context.Kategoris
                .Where(x => x.Id == input.Id.Value)
                .FirstOrDefault();

            if (mevcutKategori != null)
            {
                mevcutKategori.KategoriAd = input.KategoriAd;
                _context.Kategoris.Update(mevcutKategori);
                _context.SaveChanges();
            }
        }
        public void Sil(int id)
        {
            var kategoriler = _context.Kategoris.Find(id);

            if (kategoriler != null)
            {
                _context.Kategoris.Remove(kategoriler);
                _context.SaveChanges();
            }
        }
        private void KategoriEkle(KategoriEkleGuncelleDto input)
        {
            _context.Kategoris.Add(new Kategori
            {
                KategoriAd = input.KategoriAd,
            });

            _context.SaveChanges();
        }

    }
}
