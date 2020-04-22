using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.BusinessLogicLayer.Interfaces
{
    public interface IDealershipBusinessLogic : IGenericBusinessLogic<Dealership>
    {
        void Add(DealershipDto dealershipDto);
    }
}
