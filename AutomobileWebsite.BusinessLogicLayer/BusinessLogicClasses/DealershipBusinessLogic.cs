using AutomobileWebsite.BusinessLogicLayer.BusinessLogicsClasses;
using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.Models.Models;
using System;

namespace AutomobileWebsite.BusinessLogicLayer.BusinessLogicClasses
{
    public class DealershipBusinessLogic : GenericBusinessLogic<Dealership>, IDealershipBusinessLogic
    {
        public DealershipBusinessLogic(IGenericRepository<Dealership> repository)
            : base(repository)
        {
        }

        public void Add(DealershipDto dealershipDto)
        {
            var dealership = new Dealership
            {
                DealershipName = dealershipDto.DealershipName,
                IsActive = true,
                DateAdded = DateTime.Now
            };

            if (dealershipDto.DealershipAddressDto != null)
            {
                var dealershipAddress = new DealershipAddress
                {
                    Street = dealershipDto.DealershipAddressDto.Street,
                    City = dealershipDto.DealershipAddressDto.City,
                    StateId = dealershipDto.DealershipAddressDto.StateId,
                    ZipCode = dealershipDto.DealershipAddressDto.ZipCode,
                    IsActive = true,
                    DateAdded = DateTime.Now
                };

                dealership.DealershipAddresses.Add(dealershipAddress);
            }

            _repository.Add(dealership);
        }
    }
}
