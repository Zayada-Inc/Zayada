using Domain.Entities;

namespace Application.Dtos
{
    public class PersonalTrainersToReturnDto
    {
        public string Id { get; set; }
        public string? Certifications { get; set; }
        public string? GymName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }

    public class PersonalTrainersAdminToReturnDto
    {
        public string Id { get; set; }
        public string? Certifications { get; set; }
        public string? GymName { get; set; }
    }
}
