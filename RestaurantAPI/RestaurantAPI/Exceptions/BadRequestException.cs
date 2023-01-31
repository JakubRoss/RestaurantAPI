namespace RestaurantAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string messeage) : base(messeage) { }

    }
}