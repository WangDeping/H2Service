using Abp.Authorization;
using Abp.Localization;

namespace H2Service.Authorization
{
    public class H2ServiceAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var allPages=context.CreatePermission("Pages",L("所有权限"));

            var pages_system=allPages.CreateChildPermission(PermissionNames.Pages_System,L("系统管理"));
            pages_system.CreateChildPermission(PermissionNames.Pages_System_User, L("用户管理"));
            pages_system.CreateChildPermission(PermissionNames.Pages_System_Role, L("角色管理"));
            pages_system.CreateChildPermission(PermissionNames.Pages_System_Department, L("部门管理"));
            pages_system.CreateChildPermission(PermissionNames.Pages_System_Permission, L("权限分配"));
            pages_system.CreateChildPermission(PermissionNames.Pages_System_ExternalCompany, L("外部单位"));
            pages_system.CreateChildPermission(PermissionNames.Pages_System_ExternalUser, L("外部人员"));

            var salaryPermission=allPages.CreateChildPermission(PermissionNames.Pages_Salary,L("工资与绩效"));
            salaryPermission.CreateChildPermission(PermissionNames.Pages_Salary_Upload,L("工资导入"));
            salaryPermission.CreateChildPermission(PermissionNames.Pages_Salary_Detail, L("工资查询"));
            salaryPermission.CreateChildPermission(PermissionNames.Pages_Salary_MeritPayUpload,L("绩效导入"));
            salaryPermission.CreateChildPermission(PermissionNames.Pages_Salary_MeritPayDetail, L("绩效查询"));

            var serverRoomPermission= allPages.CreateChildPermission(PermissionNames.Pages_ServerRoom,L("机房管理"));
            serverRoomPermission.CreateChildPermission(PermissionNames.Pages_ServerRoom_Info, L("机房信息"));
            serverRoomPermission.CreateChildPermission(PermissionNames.Pages_ServerRoom_Partrol, L("机房巡视管理"));
            serverRoomPermission.CreateChildPermission(PermissionNames.Pages_ServerRoom_ServerEquipment,L("服务器/设备"));
            serverRoomPermission.CreateChildPermission(PermissionNames.Pages_ServerRoom_Event, L("机房事件"));
            serverRoomPermission.CreateChildPermission(PermissionNames.Pages_ServerRoom_ServerMonitoringInfo, L("服务器监控"));

            var QCPermission = allPages.CreateChildPermission(PermissionNames.Pages_QC,L("质量控制"));
            QCPermission.CreateChildPermission(PermissionNames.Pages_QC_FunctionalDepartment, L("考核上报"));
            QCPermission.CreateChildPermission(PermissionNames.Pages_QC_PeriodCreate, L("发布考核"));

            var infectionPermission = allPages.CreateChildPermission(PermissionNames.Pages_Infection,L("院感管理"));
            infectionPermission.CreateChildPermission(PermissionNames.Pages_Infection_MedicalWasteStatistics, L("医疗废物统计"));
            infectionPermission.CreateChildPermission(PermissionNames.Pages_Infection_MedicalWasteManage, L("医疗废物交接记录"));
            infectionPermission.CreateChildPermission(PermissionNames.Pages_Infection_MedicalWasteWorker, L("医疗废物专职人员"));
            infectionPermission.CreateChildPermission(PermissionNames.Pages_Infection_MedicalWasteWarning, L("医疗废物预警"));
            infectionPermission.CreateChildPermission(PermissionNames.Pages_Infection_MedicalWasteDeliveryHistory,L("医疗废物出库史"));
        }
        private static ILocalizableString L(string name)
        {
            
            return new LocalizableString(name, H2ServiceConsts.LocalizationSourceName);
        }
    }
}
