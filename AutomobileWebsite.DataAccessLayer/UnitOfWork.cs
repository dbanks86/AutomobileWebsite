using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutomobileWebsiteContext _context;

        public UnitOfWork(AutomobileWebsiteContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
