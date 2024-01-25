using System.ComponentModel.DataAnnotations;

namespace MVCProje.Models.DTO
{
    public class GetOyunlar
    {
        [Required]
        public int Fiyat { get; set; }
        [Required]
        public int TurId { get; set; }
        [Required]
        public int KategoriId { get; set; }
    }
}
