using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Extensions
{
 public static   class H2AbpSession
    {
        /// <summary>
        /// 获取用户姓名
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetUserName(this IAbpSession session)
        {
            var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;

            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type =="UserName");
            if (string.IsNullOrEmpty(claim?.Value))
                return null;

            return claim.Value;
        }
        /// <summary>
        /// 获取用户工号
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetUserNumber(this IAbpSession session)
        {
            var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;

            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "UserNumber");
            if (string.IsNullOrEmpty(claim?.Value))
                return null;

            return claim.Value;
        }
        public static string GetDepartmentName(this IAbpSession session)
        {
            var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;
            var departmentNameClaim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "DepartmentName");
            return departmentNameClaim.Value;
        }

        public static string GetDepartmentId(this IAbpSession session)
        {
            var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;
            var departmentIdClaim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "DepartmentId");
            return departmentIdClaim.Value;
        }
    }
}
