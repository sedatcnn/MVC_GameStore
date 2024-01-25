using MVCProje.Models.Domain;

namespace MVCProje.Models.DTO
{
    public class ShoppingCartItemViewModel
    {
        public List<Sepet> Getir { get; set; }

        public List<SepetToplam> GetirToplam { get; set;}
    }

}
