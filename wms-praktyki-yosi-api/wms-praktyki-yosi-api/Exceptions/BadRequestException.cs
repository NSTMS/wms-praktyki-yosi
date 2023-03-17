namespace wms_praktyki_yosi_api.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
