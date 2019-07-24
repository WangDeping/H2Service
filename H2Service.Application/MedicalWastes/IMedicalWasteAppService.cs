using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Account.Dto;
using H2Service.MedicalWastes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace H2Service.MedicalWastes
{
 public  interface IMedicalWasteAppService:IApplicationService
    {
        WasteFlowDto GetFlowByDepartment(int departmentId);

        void AppendWaste(AppendWasteInput input);

        DisplayWasteInDepartmentOutput GetMedicalWasteByFlowId(int flowId);

        [HttpGet]
        List<GetHandOverFlowListDto> GetHandOverFlowList(GetHandOverFlowListInput input);
       
        IEnumerable<WasteStatisticOutput> WasteStatistic(WasteStatisticInput input);

        IEnumerable<WasteStatisticOutput> GetUnDeliveryCollection(int districtId);      
        
        PagedResultDto<WasteDeliveryDto> GetPagedDeliveryHistory(GetPagedDeliveryInput input);
        /// <summary>
        /// 按院区出库
        /// </summary>
        /// <param name="districtId"></param>
        void DeliveryCollection(DeliveryCollectionInput input);

        void AppendImage(WasteImageDto dto);
        [HttpGet]
        MedicalWasteDto GetWasteByCode(string code);

        WasteTraceInfo GetTraceInfo(string code);

        List<WasteImageDto> GetImages(int flowId);

        void SetCollectUser(int flowId, long collectUserId);

        void LeaveFromDepartment(int flowId);

        void RemoveWaste(int Id);

        IEnumerable<DepartmentDto> GetDepartmentsWhoDontHaveWaste(int days);

        IEnumerable<DepartmentDto> GetDepartmentsWhoDontHandoverWaste(int days);
    }
}
