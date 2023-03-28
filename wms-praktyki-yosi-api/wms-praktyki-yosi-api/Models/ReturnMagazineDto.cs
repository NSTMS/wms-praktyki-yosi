namespace wms_praktyki_yosi_api.Models
{
    public class ReturnMagazineDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Dimentions { get; set; }
        public int ShelvesPerRow { get; set; }
        public int MaxShelfLoad { get; set; }
        public int ShelfNumber { get; set; }
        public int TotalCapacity { get; set; }
        public int TotalQuantity { get; set; }
        public int FreeSpace { get; set; }
    }
}
