using Domain.Entities;

namespace Application.Dtos
{
    public class UserToEditDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Username { get; set; }
    }
}
