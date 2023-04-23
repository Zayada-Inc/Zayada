using Domain.Entities;

namespace Domain.Specifications.Gyms
{
    public class GymsWithFilterForCountSpecification : BaseSpecification<Gym>
    {
        public GymsWithFilterForCountSpecification(GymsParam gymsParam)
            : base(x =>
                                      (string.IsNullOrEmpty(gymsParam.Search) || x.GymName.ToLower().Contains(gymsParam.Search)) &&
                                      (!gymsParam.GymId.HasValue || x.Id == gymsParam.GymId) &&
                                      (string.IsNullOrEmpty(gymsParam.SearchByAddress) || x.GymAddress.ToLower().Contains(gymsParam.SearchByAddress))
                                  )
        {
        }
    }
}