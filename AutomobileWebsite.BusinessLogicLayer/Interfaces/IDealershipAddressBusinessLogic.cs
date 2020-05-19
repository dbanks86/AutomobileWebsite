using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.BusinessLogicLayer.Interfaces
{
    public interface IDealershipAddressBusinessLogic : IGenericBusinessLogic<DealershipAddress>
    {
        void Add(DealershipAddressDto dealershipDto);
        void Update(DealershipAddress dealershipAddress, DealershipAddressDto dealershipAddressDto);
    }
}
