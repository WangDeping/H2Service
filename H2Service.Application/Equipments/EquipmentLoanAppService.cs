using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public   class EquipmentLoanAppService:H2ServiceAppServiceBase, IEquipmentLoanAppService
    {
        private readonly IRepository<EquipmentLoanLog> _loanRepoistory;

        public EquipmentLoanAppService(IRepository<EquipmentLoanLog> loanRepoistory) {
            _loanRepoistory = loanRepoistory;
        }
        /// <summary>
        /// 借出设备
        /// </summary>
        /// <param name="input"></param>
        public void LoanEqupment(LoanEquipmentInput input) {
            var pre_loanLog = _loanRepoistory.FirstOrDefault(T => T.EquipmentId == input.EquipmentId && T.SignUserId==null);
            if ( pre_loanLog!= null)
                throw new Abp.UI.UserFriendlyException(-1,"该设备处于借用状态",string.Format("{0}被{1}借用,借用人:{2},借出人:{3},借出时间:{4}",pre_loanLog.Equipment.Code,pre_loanLog.BorrowDepartment.DepartmentName,pre_loanLog.BorrowUser.UserName,pre_loanLog.LoanUser.UserName,pre_loanLog.LoanTime));
            var loanLog = input.MapTo<EquipmentLoanLog>();
            _loanRepoistory.Insert(loanLog);
        }

        public EquipmentLoanLogDto GetLoanLog(int Id) {
            return  (_loanRepoistory.FirstOrDefault(T => T.Id == Id)).MapTo<EquipmentLoanLogDto>();
        }
        /// <summary>
        /// 归还设备
        /// </summary>
        public void DoneLoanLog(DoneLoanLogInput input) {
            var loanLog = _loanRepoistory.Get(input.Id);
            if(loanLog.SignUserId!=null)
                throw new Abp.UI.UserFriendlyException(-1, "该设备未外借或已经归还，不能归还");
            loanLog.ReturnUserId = input.ReturnUserId;
            loanLog.SignUserId = input.SignUserId;
            loanLog.ReturnTime = input.ReturnTime;
            _loanRepoistory.Update(loanLog);
        }

        public PagedResultDto<EquipmentLoanLogDto> GetPagedLoanLogs(GetPagedLoanlLogsInput input)
        {
            var query = _loanRepoistory.GetAll().Where(T=>T.Equipment.DepartmentId==input.DepartmentId);
            var count = query.Count();
            var pageResult = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<EquipmentLoanLogDto> { Items = pageResult.MapTo<List<EquipmentLoanLogDto>>(), TotalCount = count };
        }
    }
}
