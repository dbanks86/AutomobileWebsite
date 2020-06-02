using AutomobileWebsite.Models.Models;
using System;

namespace AutomobileWebsite.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<State> StateRepository { get; }
        public IGenericRepository<Dealership> DealershipRepository { get; }
        public IGenericRepository<DealershipAddress> DealershipAddressRepository { get; }
        public IGenericRepository<Car> CarRepository { get; }

        void Save();
    }
}
