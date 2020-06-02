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

        private readonly Lazy<GenericBusinessLogic<State>> _lazyStateBusinessLogic;
        private readonly Lazy<DealershipBusinessLogic> _lazyDealershipBusinessLogic;
        private readonly Lazy<DealershipAddressBusinessLogic> _lazyDealershipAddressBusinessLogic;
        private readonly Lazy<CarBusinessLogic> _lazyCarBusinessLogic;

        public IGenericBusinessLogic<State> StateBusinessLogic => _lazyStateBusinessLogic.Value;
        public IDealershipBusinessLogic DealershipBusinessLogic => _lazyDealershipBusinessLogic.Value;
        public IDealershipAddressBusinessLogic DealershipAddressBusinessLogic => _lazyDealershipAddressBusinessLogic.Value;
        public ICarBusinessLogic CarBusinessLogic => _lazyCarBusinessLogic.Value;

        public BusinessLogics(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
            _lazyStateBusinessLogic = new Lazy<GenericBusinessLogic<State>>(() => new GenericBusinessLogic<State>(unitOfWork.StateRepository), true);
            _lazyDealershipBusinessLogic = new Lazy<DealershipBusinessLogic>(() => new DealershipBusinessLogic(unitOfWork.DealershipRepository), true);
            _lazyDealershipAddressBusinessLogic = new Lazy<DealershipAddressBusinessLogic>(() => new DealershipAddressBusinessLogic(unitOfWork.DealershipAddressRepository), true);
            _lazyCarBusinessLogic = new Lazy<CarBusinessLogic>(() => new CarBusinessLogic(unitOfWork.CarRepository), true);
        }

        public void Save()
        {
            _unitofWork.Save();
        }
    }
}
