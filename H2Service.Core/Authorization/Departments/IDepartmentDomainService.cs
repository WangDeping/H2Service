using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Departments
{
 public  interface IDepartmentDomainService:IDomainService
    {
      void AssginDepartment(long userId, int departmentId);

        void DeleteDepartmentUser(long userId, int departmentId);

        void DeleteDepartmentUser(int departmentId);

        bool IsGranted(int departmentId, string permissionName);


    }
}
