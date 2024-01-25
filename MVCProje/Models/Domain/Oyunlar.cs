using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProje.Models.Domain
{
    [Table("Oyunlar")]
    public class Oyunlar
    {
        public Guid Id { get; set; }
        public string OyunAdi { get; set; }
        public string Acıklama { get; set; }
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public Kategori KategoriFk { get; set; }
        public int TurId { get; set; }  
        [ForeignKey("TurId")]
        public Tur OyunTurFk { get; set; }
        public string Sirket  { get; set; }

        public decimal Fiyat { get; set; }
        public string OyunImage { get; set; }

    }
}
