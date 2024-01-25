using Microsoft.EntityFrameworkCore;
using MVCProje.Data;
using MVCProje.Models.Domain;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;

namespace MVCProje.Repositories.Implementation
{
    public class TurService : ITurService
    {
        private readonly ApplicationDbContext _context;

        public TurService (ApplicationDbContext context)
        {
            _context = context;
        }
        public Tur GetTurNyId(int turNyId)
        {
            return _context.Turs
                .Where(x => x.Id == turNyId)
                .FirstOrDefault();
        }

        public List<Tur> GetTurs()
        {
            var markalar = _context.Turs
                .OrderBy(x => x.TurAd)
                .ToList();

            return markalar;
        }

        public void TurEkleGuncell(TurEkleGuncelleDto turEkle)
        {
            if (!turEkle.Id.HasValue)
            {
                MarkaEkle(turEkle);
            }
            else
            {
                MarkaGuncelle(turEkle);
            }
        }

        private void MarkaGuncelle(TurEkleGuncelleDto turEkleGuncelle)
        {
            var mevcutMarka = _context.Turs
                .Where(x => x.Id == turEkleGuncelle.Id.Value)
                .FirstOrDefault();

            if (mevcutMarka != null)
            {
                mevcutMarka.TurAd = turEkleGuncelle.Ad;

                _context.Turs.Update(mevcutMarka);

                _context.SaveChanges();
            }
        }
        public void Sil(int id)
        {
            var tur = _context.Turs.Find(id);

            if (tur != null)
            {
                _context.Turs.Remove(tur);
                _context.SaveChanges();
            }
        }
        private void MarkaEkle(TurEkleGuncelleDto turEkleGuncelle)
        {
            _context.Turs.Add(new Tur
            {
                TurAd = turEkleGuncelle.Ad,
            });

            _context.SaveChanges();
        }
    }
}
