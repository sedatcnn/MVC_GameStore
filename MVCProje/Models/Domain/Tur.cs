    using System.ComponentModel.DataAnnotations;

namespace MVCProje.Models.Domain
{
    public class Tur
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]

        public string TurAd { get; set; }
    }
}
