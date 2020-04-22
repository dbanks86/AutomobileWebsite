using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<State> StateRepository { get; }
        public IGenericRepository<Dealership> DealershipRepository { get; }

        void Save();
    }
}
