using MVCProje.Models.Domain;

namespace MVCProje.Models.DTO
{
    public class OyunEkleGuncelleViewModel
    {
        public OyunEkleGuncelleDto Oyun { get; set; }
        public List<Kategori> Kategoriler { get; set; }
        public List<Tur> OyunTurler { get; set; }
    }
}
