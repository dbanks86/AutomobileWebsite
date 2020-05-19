namespace AutomobileWebsite.BusinessLogicLayer.Dtos
{
    public class DealershipAddressDto
    {
        public int DealershipId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
    }
}
