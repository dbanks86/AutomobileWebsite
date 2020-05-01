﻿using AutomobileWebsite.BusinessLogicLayer.BusinessLogicsClasses;
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
                WebsiteUrl = dealershipDto.WebsiteUrl,
                IsActive = true,
                DateAdded = DateTime.Now
            };

            _repository.Add(dealership);
        }
    }
}
