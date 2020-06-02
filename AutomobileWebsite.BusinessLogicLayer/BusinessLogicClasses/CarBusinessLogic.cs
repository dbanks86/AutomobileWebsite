using AutomobileWebsite.BusinessLogicLayer.BusinessLogicsClasses;
using AutomobileWebsite.BusinessLogicLayer.Dtos;
using AutomobileWebsite.BusinessLogicLayer.Interfaces;
using AutomobileWebsite.DataAccessLayer.Interfaces;
using AutomobileWebsite.Models.Models;

namespace AutomobileWebsite.BusinessLogicLayer.BusinessLogicClasses
{
    public class CarBusinessLogic : GenericBusinessLogic<Car>, ICarBusinessLogic
    {
        public CarBusinessLogic(IGenericRepository<Car> repository)
            : base(repository)
        {
        }

        public void Add(CarDto carDto)
        {
            var car = new Car
            {
                Year = carDto.Year,
                Make = carDto.Make,
                Model = carDto.Model,
                Trim = carDto.Trim,
                Mileage = carDto.Mileage,
                Price = carDto.Price,
                IsNew = carDto.IsNew,
                DealershipAddressId = carDto.DealershipAddressId,
                IsActive = true
            };

            _repository.Add(car);
        }
    }
}
