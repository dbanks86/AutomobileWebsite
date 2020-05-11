using AutomobileWebsite.BusinessLogicLayer.BusinessLogicClasses;
using AutomobileWebsite.BusinessLogicLayer.BusinessLogicsClasses;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.Models.Models;
using System;

namespace AutomobileWebsite.BusinessLogicLayer
{
    public class BusinessLogics : IBusinessLogics
    {
        private readonly IUnitOfWork _unitofWork;

        private readonly Lazy<GenericBusinessLogic<State>> _stateBusinessLogic;
        private readonly Lazy<DealershipBusinessLogic> _dealershipBusinessLogic;
        private readonly Lazy<DealershipAddressBusinessLogic> _dealershipAddressBusinessLogic;

        public IGenericBusinessLogic<State> StateBusinessLogic => _stateBusinessLogic.Value;
        public IDealershipBusinessLogic DealershipBusinessLogic => _dealershipBusinessLogic.Value;
        public IDealershipAddressBusinessLogic DealershipAddressBusinessLogic => _dealershipAddressBusinessLogic.Value;

        public BusinessLogics(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
            _stateBusinessLogic = new Lazy<GenericBusinessLogic<State>>(() => new GenericBusinessLogic<State>(unitOfWork.StateRepository), true);
            _dealershipBusinessLogic = new Lazy<DealershipBusinessLogic>(() => new DealershipBusinessLogic(unitOfWork.DealershipRepository), true);
            _dealershipAddressBusinessLogic = new Lazy<DealershipAddressBusinessLogic>(() => new DealershipAddressBusinessLogic(unitOfWork.DealershipAddressRepository), true);
        }

        public void Save()
        {
            _unitofWork.Save();
        }
    }
}
