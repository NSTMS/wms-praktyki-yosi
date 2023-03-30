using System.Data;

namespace wms_praktyki_yosi_api.Enitities
{
    public class Magazine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Dimentions { get; set; }
        public int ShelvesPerRow { get; set; }
        public bool Deleted { get; set; }
        public virtual List<Shelf> Shelves { get; set; }
    }
}
