namespace RestaurantAPI.Entities
{
    public class Address
    {
        public int Id { get; set; }
#nullable enable
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
#nullable disable
        public virtual Restaurant Restaurant { get; set; }
    }
}