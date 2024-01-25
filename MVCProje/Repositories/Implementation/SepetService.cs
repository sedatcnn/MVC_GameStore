using MVCProje.Data;
using MVCProje.Models.Domain;
using MVCProje.Models.DTO;
using MVCProje.Repositories.Abstract;

namespace MVCProje.Repositories.Implementation
{
    public class SepetService : ISepet
    {
        private readonly ApplicationDbContext _context;
        public SepetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Sepet GetSepetById(Guid id)
        {
            return _context.Sepets.Where(x=>x.Id==id).FirstOrDefault();

        }
        public bool IsInCart(Guid oyunId)
        {
            var cartItems = GetSepets(); 
            return cartItems.Any(item => item.OyunId == oyunId);
        }
        public List<Sepet> GetSepets()
        {
            var sepet=_context.Sepets.OrderBy(x=>x.OyunAdi).ToList();
            return sepet;
        }

        public void SepetEkleGuncelle(Sepet guncelle)
        {
           
             SepetGuncelle(guncelle);
         
        }
        public void SepetGuncelle(Sepet sepet)
        {
            var mevcutSepet = _context.Sepets
                .Where(x => x.OyunId == sepet.OyunId)
                .FirstOrDefault();

            if (mevcutSepet == null)
           
            {
                _context.Sepets.Add(new Sepet
                {
                    Id = Guid.NewGuid(),
                    OyunId = sepet.OyunId,
                    OyunAdi = sepet.OyunAdi,
                    Fiyat = sepet.Fiyat,
                    OyunImage = sepet.OyunImage,
                });
            }

            _context.SaveChanges();
        }


        public decimal GetTotalAmount()
        {
            return _context.Sepets.Sum(item => item.Fiyat);
        }
        public decimal Kazanc()
        {
            return _context.SepetToplams.Sum(item => item.FiyatToplam);
        }
        public void SaveOrderDetails(decimal totalAmount)
        {
            var orderDetails = new SepetToplam
            {
                Id = Guid.NewGuid(),
                FiyatToplam = totalAmount
            };

            _context.SepetToplams.Add(orderDetails);
            _context.SaveChanges();
        }
        public void Sil(Guid id)
        {
            var sepet = _context.Sepets.Find(id);
            if (sepet != null)
            {
                _context.Sepets.Remove(sepet);
                _context.SaveChanges();
            }
        }
        public void ClearSepet()
        {
            var sepetItems = _context.Sepets.ToList();  

            foreach (var item in sepetItems)
            {
                _context.Sepets.Remove(item);
            }

            _context.SaveChanges();
        }
    }
}
