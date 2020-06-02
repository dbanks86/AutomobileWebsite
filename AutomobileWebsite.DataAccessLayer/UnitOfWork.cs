using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Repositories;
using AutomobileWebsite.Models.Models;
using System;

namespace AutomobileWebsite.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutomobileWebsiteContext _context;

        private readonly Lazy<IGenericRepository<State>> _lazyStateRepository;
        private readonly Lazy<IGenericRepository<Dealership>> _lazyDealershipRepository;
        private readonly Lazy<IGenericRepository<DealershipAddress>> _lazyDealershipAddressRepository;
        private readonly Lazy<IGenericRepository<Car>> _lazyCarRepository;
        private bool disposed = false;

        public IGenericRepository<State> StateRepository => _lazyStateRepository.Value;
        public IGenericRepository<Dealership> DealershipRepository => _lazyDealershipRepository.Value;
        public IGenericRepository<DealershipAddress> DealershipAddressRepository => _lazyDealershipAddressRepository.Value;
        public IGenericRepository<Car> CarRepository => _lazyCarRepository.Value;

        public UnitOfWork(AutomobileWebsiteContext context)
        {
            _context = context;
            _lazyStateRepository = new Lazy<IGenericRepository<State>>(() => new GenericRepository<State>(context) ,true);
            _lazyDealershipRepository = new Lazy<IGenericRepository<Dealership>>(() => new GenericRepository<Dealership>(context), true);
            _lazyDealershipAddressRepository = new Lazy<IGenericRepository<DealershipAddress>>(() => new GenericRepository<DealershipAddress>(context), true);
            _lazyCarRepository = new Lazy<IGenericRepository<Car>>(() => new GenericRepository<Car>(context), true);
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
