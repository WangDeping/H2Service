using Abp.AutoMapper;
using H2Service.Salarires;

namespace H2Service.Salaries.Dto
{
    [AutoMap(typeof(SalaryDetail))]
    public   class PersonalSalaryOutput
    {
        public string UserNumber { get; set; }

        public SalaryType SalaryType { get; set; }

        public string Detail { get; set; }
    }
}
