namespace wms_praktyki_yosi_api.Enitities
{
    public class Shelf
    {
        public int Id { get; set; } 
        public int MaxLoad { get; set; }
        public string Position { get; set; }
        public int MagazineId { get; set; }
        public virtual List<ProductLocations> Locations { get; set; }

        public byte[] Version { get; set; }
    }
}
