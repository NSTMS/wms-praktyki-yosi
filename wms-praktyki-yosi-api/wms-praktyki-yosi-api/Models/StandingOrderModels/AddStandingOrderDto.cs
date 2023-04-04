using wms_praktyki_yosi_api.Models.StandingOrderModels;

namespace wms_praktyki_yosi_api.Enitities
{
	public class AddStandingOrderDto
	{
		public long Interval { get; set; }
		public string Client { get; set; }
		public int MagazineId { get; set; }
		public List<AddOrderItemDto> Items { get; set; }

	}
}
