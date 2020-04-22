using AutomobileWebsite.BusinessLogicLayer.BusinessLogicClasses;
using AutomobileWebsite.BusinessLogicLayer.BusinessLogicsClasses;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.BusinessLogicLayer
{
    public class BusinessLogics : IBusinessLogics
    {
        private readonly IUnitOfWork _unitofWork;

        public IGenericBusinessLogic<State> StateBusinessLogic { get; private set; }
        public IDealershipBusinessLogic DealershipBusinessLogic { get; private set; }

        public BusinessLogics(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
            StateBusinessLogic = new GenericBusinessLogic<State>(unitOfWork.StateRepository);
            DealershipBusinessLogic = new DealershipBusinessLogic(unitOfWork.DealershipRepository);
        }

        public void Save()
        {
            _unitofWork.Save();
        }
    }
}
