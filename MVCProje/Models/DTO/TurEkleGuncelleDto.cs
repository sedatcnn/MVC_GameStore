using System.ComponentModel.DataAnnotations;

namespace MVCProje.Models.DTO
{
    public class TurEkleGuncelleDto
    {
        public int? Id { get; set; }
        [Required]
        public string Ad { get; set; }
    }
}
