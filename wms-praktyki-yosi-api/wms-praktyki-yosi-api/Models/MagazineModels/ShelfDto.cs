namespace wms_praktyki_yosi_api.Models.MagazineModels
{
    public class ShelfDto
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public int MaxQuantity { get; set; }
        public int TotalQuantity { get; set; }
        public int FreeSpace { get; set; }
    }
}
