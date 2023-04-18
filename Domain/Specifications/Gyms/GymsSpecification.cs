using Domain.Entities;

namespace Domain.Specifications.Gyms
{
    public class GymsSpecification : BaseSpecification<Gym>
    {
        public GymsSpecification() { }
        public GymsSpecification(int id) : base(x => x.Id == id)
        {
        }

        public GymsSpecification(GymsParam gymsParam) : base(x =>
                                         (string.IsNullOrEmpty(gymsParam.Search) || x.GymName.ToLower().Contains(gymsParam.Search)) &&
                                         (!gymsParam.GymId.HasValue || x.Id == gymsParam.GymId) &&
                                         (string.IsNullOrEmpty(gymsParam.SearchByAddress) || x.GymAddress.ToLower().Contains(gymsParam.SearchByAddress))
                                     )
        {
            ApplyPaging(gymsParam.PageSize * (gymsParam.PageIndex - 1), gymsParam.PageSize);
            AddOrderBy(n => n.GymName);
            if (!string.IsNullOrEmpty(gymsParam.Sort))
            {
                switch (gymsParam.Sort)
                {
                    case "gymAsc":
                        AddOrderBy(p => p.GymName.ToLower());
                        break;
                    case "gymDesc":
                        AddOrderByDescending(p => p.GymName.ToLower());
                        break;
                    case "idAsc":
                        AddOrderBy(p => p.Id);
                        break;
                    
                    default:
                        AddOrderBy(n => n.GymName.ToLower());
                        break;
                }
            }
        }
    }
}
