using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Interfaces;

namespace AutomobileWebsite.BusinessLogicLayer
{
    public class BusinessLogics : IBusinessLogics
    {
        private readonly IUnitOfWork _unitofWork;

        public BusinessLogics(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        public void Save()
        {
            _unitofWork.Save();
        }
    }
}
