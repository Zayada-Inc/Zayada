using AutoMapper;
using Domain.Entities;
using ZayadaAPI.Controllers;
using ZayadaAPI.Dtos;

namespace ZayadaAPI.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<PersonalTrainer, PersonalTrainersToReturnDto>()
            .ForMember(d => d.GymName, o => o.MapFrom(s => s.Gym.GymName))
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

            CreateMap<PersonalTrainersToPost, PersonalTrainer>()
                .ForMember(d => d.Gym, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());
            CreateMap<Gym, GymsToReturnDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));
            CreateMap<GymsToPostDto, Gym>()
                .ForMember(d => d.Id, o => o.Ignore());
                
        }
    }
}
