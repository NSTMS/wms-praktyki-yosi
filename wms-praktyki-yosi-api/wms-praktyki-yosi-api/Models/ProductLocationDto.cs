﻿using System.Reflection.Metadata.Ecma335;
using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models
{
    public class ProductLocationDto
    {
        public int ProductId { get; set; }
        public int MagazineId { get; set; }
        public string Position { get; set; }
        public int Quantity { get; set; } // na sztywno
    }
}