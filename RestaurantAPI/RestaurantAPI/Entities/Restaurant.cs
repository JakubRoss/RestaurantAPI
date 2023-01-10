namespace RestaurantAPI.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
#nullable enable
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
#nullable disable
        public int AdressId { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual List<Dish> Dishes { get; set; }

    }


}
