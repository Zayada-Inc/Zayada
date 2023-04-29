using Domain.Entities.IdentityEntities;

namespace Domain.Entities
{
    public class PaymentToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public bool IsUsed { get; set; }
        public AppUser User { get; set; }
    }
}
