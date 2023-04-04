using System.Linq.Expressions;
using wms_praktyki_yosi_api.Models.AccountModels;
using wms_praktyki_yosi_api.Models.DocumentModels;
using wms_praktyki_yosi_api.Models.MagazineModels;
using wms_praktyki_yosi_api.Models.ProductModels;
using wms_praktyki_yosi_api.Models.StandingOrderModels;

namespace wms_praktyki_yosi_api.Services.Static
{
    static public class OrderByColumnSelectors
    {
        public static readonly Dictionary<string, Expression<Func<UserDto, object>>> Users = new()
        {
            {nameof(UserDto.Email).ToLower(), p =>  p.Email},
            {nameof(UserDto.Role).ToLower(), p => p.Role},
        };

        public static readonly Dictionary<string, Expression<Func<DocumentDto, object>>> Documents = new()
        {
            {nameof(DocumentDto.Date).ToLower(), p =>  p.Date},
            {nameof(DocumentDto.Client).ToLower(), p => p.Client },
            {nameof(DocumentDto.TotalQuantity).ToLower(), p => p.TotalQuantity},
            {nameof(DocumentDto.QuantityDone).ToLower(), p => p.QuantityDone},
        };

        public static readonly Dictionary<string, Expression<Func<DocumentItemDto, object>>> DocumentItems = new()
        {
            {nameof(DocumentItemDto.ProductName).ToLower(), p =>  p.ProductName},
            {nameof(DocumentItemDto.Position).ToLower(), p => p.Position[0] - 'A' + p.Position[1] - '0' },
            {nameof(DocumentItemDto.QuantityPlaned).ToLower(), p => p.QuantityPlaned},
            {nameof(DocumentItemDto.QuantityDone).ToLower(), p => p.QuantityDone},
            {nameof(DocumentItemDto.Tag).ToLower(), p => p.Tag},
        };

        public static readonly Dictionary<string, Expression<Func<ReturnMagazineDto, object>>> Magazines = new()
        {
            {nameof(ReturnMagazineDto.Name).ToLower(), p =>  p.Name},
            {nameof(ReturnMagazineDto.Dimentions).ToLower(), p => p.Dimentions },
            {nameof(ReturnMagazineDto.ShelvesPerRow).ToLower(), p => p.ShelvesPerRow},
            {nameof(ReturnMagazineDto.MaxShelfLoad).ToLower(), p => p.MaxShelfLoad},
            {nameof(ReturnMagazineDto.ShelfNumber).ToLower(), p => p.ShelfNumber},
            {nameof(ReturnMagazineDto.TotalCapacity).ToLower(), p => p.TotalCapacity},
            {nameof(ReturnMagazineDto.TotalQuantity).ToLower(), p => p.TotalQuantity},
            {nameof(ReturnMagazineDto.FreeSpace).ToLower(), p => p.FreeSpace},
        };

        public static readonly Dictionary<string, Expression<Func<ProductDto, object>>> Products = new()
        {
            {nameof(ProductDto.ProductName).ToLower(), p =>  p.ProductName},
            {nameof(ProductDto.EAN).ToLower(), p => p.EAN },
            {nameof(ProductDto.Price).ToLower(), p => p.Price},
            {nameof(ProductDto.Quantity).ToLower(), p => p.Quantity},
        };

        public static readonly Dictionary<string, Expression<Func<ReturnProductLocationDto, object>>> Locations = new()
        {
            {nameof(ReturnProductLocationDto.ProductId).ToLower(), p =>  p.ProductId},
            {nameof(ReturnProductLocationDto.Quantity).ToLower(), p => p.Quantity},
            {nameof(ReturnProductLocationDto.Tag).ToLower(), p => p.Tag},
        };

        public static readonly Dictionary<string, Expression<Func<ShelfDto, object>>> Shelves = new()
        {
            {nameof(ShelfDto.Position).ToLower(), p =>  p.Position},
            {nameof(ShelfDto.TotalQuantity).ToLower(), p => p.TotalQuantity},
            {nameof(ShelfDto.FreeSpace).ToLower(), p => p.FreeSpace},
            {nameof(ShelfDto.MaxQuantity).ToLower(), p => p.MaxQuantity},
        };

        public static readonly Dictionary<string, Expression<Func<StandingOrderDto, object>>> Orders = new()
        {
            {nameof(StandingOrderDto.Interval).ToLower(), p =>  p.Interval},
            {nameof(StandingOrderDto.NextOrder).ToLower(), p => p.NextOrder},
            {nameof(StandingOrderDto.Client).ToLower(), p => p.Client},
            {nameof(StandingOrderDto.MagazineId).ToLower(), p => p.MagazineId},
            {nameof(StandingOrderDto.TotalQuantity).ToLower(), p => p.TotalQuantity},
        };

        public static readonly Dictionary<string, Expression<Func<OrderItemDto, object>>> OrderItems = new()
        {
            {nameof(OrderItemDto.ProductName).ToLower(), p =>  p.ProductName },
            {nameof(OrderItemDto.Quantity).ToLower(), p => p.Quantity },
            {nameof(OrderItemDto.Tag).ToLower(), p => p.Tag ?? "" },
        };

    }
}
