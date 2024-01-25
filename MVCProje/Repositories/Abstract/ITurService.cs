using MVCProje.Models.Domain;
using MVCProje.Models.DTO;

namespace MVCProje.Repositories.Abstract
{
    public interface ITurService
    {
        List<Tur> GetTurs();

        void TurEkleGuncell(TurEkleGuncelleDto  turEkle);
        void Sil(int id);

        Tur GetTurNyId(int turNyId);

    }
}
