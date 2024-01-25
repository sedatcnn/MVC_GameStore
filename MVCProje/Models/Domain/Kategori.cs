using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProje.Models.Domain
{
    [Table("Kategori")]
    public class Kategori
    {
        public int Id { get; set; }

        public string KategoriAd { get; set; }
    }
}
