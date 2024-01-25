using MVCProje.Models.Domain;
using MVCProje.Models.DTO;

namespace MVCProje.Repositories.Abstract
{
    public interface ISepet
    {
        List<Sepet> GetSepets();
        void SepetEkleGuncelle(Sepet guncelle);

        void Sil(Guid id);
        bool IsInCart(Guid oyunId);

        decimal GetTotalAmount();
        decimal Kazanc();
        void SaveOrderDetails(decimal totalAmount);

        void ClearSepet();

        Sepet GetSepetById(Guid id);
    }
}
