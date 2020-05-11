using AutomobileWebsite.BusinessLogicLayer.BusinessLogicsClasses;
using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.Models.Models;
using System;

namespace AutomobileWebsite.BusinessLogicLayer.BusinessLogicClasses
{
    public class DealershipAddressBusinessLogic : GenericBusinessLogic<DealershipAddress>, IDealershipAddressBusinessLogic
    {
        public DealershipAddressBusinessLogic(IGenericRepository<DealershipAddress> repository)
            : base(repository)
        {
        }

        public void Add(DealershipAddressDto dealershipDto)
        {
            _repository.Add(new DealershipAddress
            {
                DealershipId = dealershipDto.DealershipId,
                Street = dealershipDto.Street,
                City = dealershipDto.City,
                StateId = dealershipDto.StateId,
                ZipCode = dealershipDto.ZipCode,
                IsActive = true,
                DateAdded = DateTime.Now
            });
        }
    }
}
