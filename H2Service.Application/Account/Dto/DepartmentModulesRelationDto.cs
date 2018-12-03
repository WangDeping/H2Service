using Abp.AutoMapper;
using H2Service.Authorization.Departments;

namespace H2Service.Account.Dto
{
    [AutoMap(typeof(DepartmentRelateModule))]
    public   class DepartmentModulesRelationDto
    {
        public int Id { get; set; }

        public H2Module Module { get; set; }

        public int DepartmentId { get; set; }
       
        public string DepartmentName { get; set; }
    }
}