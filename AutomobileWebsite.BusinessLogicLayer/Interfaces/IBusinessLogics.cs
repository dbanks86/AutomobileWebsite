using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.BusinessLogicLayer.Interfaces
{
    public interface IBusinessLogics
    {
        IGenericBusinessLogic<State> StateBusinessLogic { get; }
        IDealershipBusinessLogic DealershipBusinessLogic { get; }
        IDealershipAddressBusinessLogic DealershipAddressBusinessLogic { get; }
        ICarBusinessLogic CarBusinessLogic { get; }

        void Save();
    }
}
