using Domain.Entities;

namespace Application.Repositories
{
    public interface IChemicalRepository : IGenericRepository<Chemical>
    {
        List<Chemical> GetTop3LatestChemical();
    }
}
