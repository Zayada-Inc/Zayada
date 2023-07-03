using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.CommandsQueries.Gyms
{
    public class GymEmployeesQuery
    {
         public class Query : IRequest<IEnumerable<EmployeeToReturnDto>>
        {
        }

         public class Handler : IRequestHandler<Query, IEnumerable<EmployeeToReturnDto>>
        {
            private readonly IGymService _gymService;
            public Handler(IGymService gymService)
            {
                _gymService = gymService;
            }
                public async Task<IEnumerable<EmployeeToReturnDto>> Handle(Query request, CancellationToken cancellationToken)
                {
                return await _gymService.GetEmployeesForCurrentGymAsync();
                }
        }
    }
}
