using MVCProje.Models.Domain;
using MVCProje.Models.DTO;

namespace MVCProje.Repositories.Abstract
{
    public interface IKategoriService
    {
        List<Kategori> GetKategoriler();
        void KategoriEkleGuncelle(KategoriEkleGuncelleDto guncelle);
        void Sil(int id);

        Kategori GetKategoribyId(int id);
    }
}
