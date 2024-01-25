using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProje.Models.Domain
{
    [Table("Sepet")]
    public class Sepet
    {
        public Guid Id { get; set; }
        public Guid OyunId { get; set; }
        public string OyunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public string OyunImage { get; set; }
    }

    public class SepetToplam
    {
        public Guid Id { get; set; }

        public decimal FiyatToplam { get; set; }
    }
}
