﻿namespace Domain.Entities
{
    public class PersonalTrainer : EntityBase
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? InstagramLink { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Certifications { get; set; }

    }
}