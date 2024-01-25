using MVCProje.Models.Domain;

namespace MVCProje.Models.DTO
{
    public class HomeIndexViewModel
    {
        public int TurId { get; set; }
        public int KategoriId { get; set; }
        public List<Tur> Turler { get; set; }
        public List<Kategori> Kategoriler { get; set; }
        public List<OyunDto> Oyunlar { get; set; }
        public string Sıralama { get; set; }
        public int SayfaSayısı { get; set; }
        public int SayfaBoyutu { get; set; }
        public int ToplamSayfa { get; set; }

    }
}
