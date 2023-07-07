using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Infrastructure.Dtos;

namespace Application.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<PersonalTrainer, PersonalTrainersToReturnDto>()
            .ForMember(d => d.GymName, o => o.MapFrom(s => s.Gym.GymName))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.User.UserName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(d => d.Photos, o => o.MapFrom(s => s.User.Photos))
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

            CreateMap<PersonalTrainer, PersonalTrainersAdminToReturnDto>()
                .ForMember(d => d.GymName, o => o.MapFrom(s => s.Gym.GymName))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Certifications, o => o.MapFrom(s => s.Certifications));

            CreateMap<PersonalTrainersToPost, PersonalTrainer>()
                .ForMember(d => d.Gym, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<Gym, GymsToReturnDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

            CreateMap<GymsToPostDto, Gym>()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<GymsToEditDto,Gym>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.GymName, o => o.MapFrom(s => s.GymName))
                .ForMember(d => d.GymAddress, o => o.MapFrom(s => s.GymAddress));

            CreateMap<Gym, GymsToEditDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.GymName, o => o.MapFrom(s => s.GymName))
                .ForMember(d => d.GymAddress, o => o.MapFrom(s => s.GymAddress));

            CreateMap<SubscriptionPlanToPostDto, SubscriptionPlan>()
                .ForMember(d => d.Gym, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<SubscriptionPlan, SubscriptionPlanToReturnDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.GymId, o => o.MapFrom(s => s.GymId))
                .ForMember(d => d.DurationInDays, o => o.MapFrom(s => s.DurationInDays))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));

            CreateMap<AppUser, UserReturnDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.DisplayName))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.Photos, o => o.MapFrom(s => s.Photos.AsEnumerable().Where(x => x.IsMain == true)))
                .ForMember(d => d.PersonalTrainer, o => o.MapFrom(s => s.PersonalTrainer));

        }
    }
}
