using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling.Dto
{
    /// <summary>
    /// 人员与排班科室关系
    /// </summary>
    [AutoMap(typeof(SchedulingDepartmentUser))]
    public   class SchedulingDepartmentUserDto
    {
        /// <summary>
        /// 关系Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 科室Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 人员Id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 人员工号
        /// </summary>
        public string UserNumber { get; set; }

    }
}
