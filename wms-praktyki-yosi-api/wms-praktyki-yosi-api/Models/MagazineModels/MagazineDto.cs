namespace wms_praktyki_yosi_api.Models.MagazineModels
{
    public class MagazineDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Dimentions { get; set; }
        public int ShelvesPerRow { get; set; }
        public int MaxShelfLoad { get; set; } = 100;
    }
}