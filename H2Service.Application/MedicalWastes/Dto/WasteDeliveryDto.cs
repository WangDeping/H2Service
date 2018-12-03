using Abp.AutoMapper;

namespace H2Service.MedicalWastes.Dto
{
    [AutoMap(typeof(MedicalWasteDelivery))]
    public   class WasteDeliveryDto
    {
        public int Id { get; set; }

        public string DistrictName { get; set; }

        public int DistrictId { get; set; }

        public string Summary { get; set; }

        public string CreatorUserName { get; set; }

        public string CreationTime { get; set; }
    }
}