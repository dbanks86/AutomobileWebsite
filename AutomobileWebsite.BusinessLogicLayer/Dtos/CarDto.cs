namespace AutomobileWebsite.BusinessLogicLayer.Dtos
{
    public class CarDto
    {
        public short Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public int DealershipAddressId { get; set; }
    }
}
