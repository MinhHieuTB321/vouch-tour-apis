using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
