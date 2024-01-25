using MVCProje.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProje.Models.DTO
{
    public class OyunDto
    {
        public Guid Id { get; set; }
        [Required]
        public string OyunAdi { get; set; }
        [Required]
        public string Acıklama { get; set; }
        public string KategoriAdi { get; set; }
        public string TurAdi { get; set; }

        [Required]

        public string Sirket { get; set; }

        [Required]
        public decimal Fiyat { get; set; }
        [Required]
        public string OyunImage { get; set; }

    }
}
