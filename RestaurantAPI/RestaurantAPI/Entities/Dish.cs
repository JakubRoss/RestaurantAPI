namespace RestaurantAPI.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
#nullable enable
        public string? Description { get; set; }
        public decimal Price { get; set; }
#nullable disable
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }
}