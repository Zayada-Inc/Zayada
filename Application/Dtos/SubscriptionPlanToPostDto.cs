namespace Application.Dtos
{
    public class SubscriptionPlanToPostDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int DurationInDays { get; set; }
        public int GymId { get; set; }
    }
}
