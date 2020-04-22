using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Repositories;
using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutomobileWebsiteContext _context;

        public IGenericRepository<State> StateRepository { get; private set; }
        public IGenericRepository<Dealership> DealershipRepository { get; private set; }

        public UnitOfWork(AutomobileWebsiteContext context)
        {
            _context = context;
            StateRepository = new GenericRepository<State>(context);
            DealershipRepository = new GenericRepository<Dealership>(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
