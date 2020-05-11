using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Repositories;
using AutomobileWebsite.Models.Models;
using System;

namespace AutomobileWebsite.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutomobileWebsiteContext _context;

        private readonly Lazy<IGenericRepository<State>> _stateRepository;
        private readonly Lazy<IGenericRepository<Dealership>> _dealershipRepository;
        private readonly Lazy<IGenericRepository<DealershipAddress>> _dealershipAddressRepository;
        private bool disposed = false;

        public IGenericRepository<State> StateRepository => _stateRepository.Value;
        public IGenericRepository<Dealership> DealershipRepository => _dealershipRepository.Value;
        public IGenericRepository<DealershipAddress> DealershipAddressRepository => _dealershipAddressRepository.Value;

        public UnitOfWork(AutomobileWebsiteContext context)
        {
            _context = context;
            _stateRepository = new Lazy<IGenericRepository<State>>(() => new GenericRepository<State>(context) ,true);
            _dealershipRepository = new Lazy<IGenericRepository<Dealership>>(() => new GenericRepository<Dealership>(context), true);
            _dealershipAddressRepository = new Lazy<IGenericRepository<DealershipAddress>>(() => new GenericRepository<DealershipAddress>(context), true);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
