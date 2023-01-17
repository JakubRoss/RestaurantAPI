namespace RestaurantAPI.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string messeage) : base(messeage) { }

    }
}