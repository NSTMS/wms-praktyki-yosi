namespace wms_praktyki_yosi_api.Enitities
{
    public class Shelves
    {
        public int Id { get; set; } 
        public string Position { get; set; }
        public int MagazineId { get; set; }
        public List<ProductLocations> Locations { get; set; }
    }
}
