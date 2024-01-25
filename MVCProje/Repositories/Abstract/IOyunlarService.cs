using MVCProje.Models.Domain;
using MVCProje.Models.DTO;

namespace MVCProje.Repositories.Abstract
{
    public interface IOyunlarService
    {
        List<OyunDto> GetOyunDtos(GetOyunlar getOyunlar,string sıralama);

        List<Oyunlar> GetOyunlar();
        void OyunEkleGuncelle(OyunEkleGuncelleDto oyuDto);
        void Sil(Guid id);
        int GetTotalOyunCount();

        Oyunlar GetOyunById(Guid id);
    }
}
