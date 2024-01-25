public class OyunEkleGuncelleDto
{
    public Guid? Id { get; set; }
    public string OyunAdi { get; set; }
    public string Aciklama { get; set; }
    public int KategoriId { get; set; }
    public int TurId { get; set; }
    public string Sirket { get; set; }
    public decimal Fiyat { get; set; }
    public string OyunImage { get; set; }
    public IFormFile ResimDosyasi { get; set; }
    public bool KullanMevcutResim { get; set; }



}
