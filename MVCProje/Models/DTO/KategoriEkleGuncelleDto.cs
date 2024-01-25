using Microsoft.Build.Framework;
namespace MVCProje.Models.DTO
{
    public class KategoriEkleGuncelleDto
    {
        public int? Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Açıklama boş bırakılamaz.")]

        public string KategoriAd { get; set; }
    }
}
