using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.BusinessLogicLayer.Interfaces
{
    public interface IBusinessLogics
    {
        IGenericBusinessLogic<State> StateBusinessLogic { get; }
        IDealershipBusinessLogic DealershipBusinessLogic { get; }

        void Save();
    }
}
