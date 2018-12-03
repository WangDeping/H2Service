using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using H2Service.Authorization;

namespace H2Service.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class H2ServiceNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {

            context.Manager.MainMenu.AddItem(
                new MenuItemDefinition(
                    "收入查询",
                    new LocalizableString("工资与绩效", H2ServiceConsts.LocalizationSourceName),
                    url: "#/",
                    icon: "flaticon-piggy-bank "
                    ).AddItem(new MenuItemDefinition(
                        "工资导入",
                        new LocalizableString("工资导入", H2ServiceConsts.LocalizationSourceName),
                        url: "Salary/Index",
                        icon: "la la-money",
                        requiredPermissionName: PermissionNames.Pages_Salary_Upload
                        )
                   ).AddItem(new MenuItemDefinition(
                         "工资查询",
                        new LocalizableString("工资查询", H2ServiceConsts.LocalizationSourceName),
                        url: "Salary/PersonalPeriodIndex",
                        icon: "la la-reorder"
                       )
                       ).AddItem(new MenuItemDefinition(
                        "绩效导入",
                        new LocalizableString("绩效导入", H2ServiceConsts.LocalizationSourceName),
                        url: "MeritPay/",
                        icon: "la la-money",
                        requiredPermissionName: PermissionNames.Pages_Salary_MeritPayUpload
                        )
                   ).AddItem(new MenuItemDefinition(
                        "绩效查询",
                        new LocalizableString("绩效查询", H2ServiceConsts.LocalizationSourceName),
                        url: "MeritPay/PersonalIndex",
                        icon: "la la-reorder"
                        )
                   ).AddItem(new MenuItemDefinition(
                        "绩效查询",
                        new LocalizableString("本科室绩效", H2ServiceConsts.LocalizationSourceName),
                        url: "MeritPay/HeaderIndex",
                        icon: "la la-reorder"
                        )
                   )
                ).AddItem(
                new MenuItemDefinition(
                    "质量控制",
                    new LocalizableString("质量控制", H2ServiceConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "flaticon-cogwheel",
                        requiredPermissionName: PermissionNames.Pages_QC
                    ).AddItem(new MenuItemDefinition(
                        "发布督察任务",
                        new LocalizableString("发布督察任务", H2ServiceConsts.LocalizationSourceName),
                        url: "QC/CreatePeriod",
                        icon: "flaticon-users",
                        requiredPermissionName: PermissionNames.Pages_QC_PeriodCreate
                        )
                   ).AddItem(new MenuItemDefinition(
                       "督察填报",
                        new LocalizableString("督察填报", H2ServiceConsts.LocalizationSourceName),
                        url: "QC/FDIndex",
                        icon: "flaticon-network",
                        requiredPermissionName: PermissionNames.Pages_QC_FunctionalDepartment
                       )
                   )
                ).AddItem(
                new MenuItemDefinition(
                    "院感管理",
                    new LocalizableString("院感管理", H2ServiceConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "flaticon-technology-1",
                        requiredPermissionName: PermissionNames.Pages_Infection
                    ).AddItem(new MenuItemDefinition(
                        "医疗废物交接记录",
                        new LocalizableString("医疗废物交接记录", H2ServiceConsts.LocalizationSourceName),
                        url: "MedicalWaste/Index",
                        icon: "la la-delicious",
                        requiredPermissionName: PermissionNames.Pages_Infection_MedicalWasteManage
                        )
                   ).AddItem(new MenuItemDefinition(
                         "医疗废物统计",
                         new LocalizableString("医疗废物统计", H2ServiceConsts.LocalizationSourceName),
                         url: "MedicalWaste/StatisticsIndex",
                         icon: "la la-bar-chart",
                         requiredPermissionName: PermissionNames.Pages_Infection_MedicalWasteStatistics
                         )
                    ).AddItem(new MenuItemDefinition(
                         "医疗废物暂存点",
                         new LocalizableString("医疗废物暂存点", H2ServiceConsts.LocalizationSourceName),
                         url: "District/DistrictWithMedicalWaste",
                         icon: "flaticon-signs",
                         requiredPermissionName: PermissionNames.Pages_Infection_MedicalWasteWorker
                         )
                    ).AddItem(new MenuItemDefinition(
                         "医疗废物预警",
                         new LocalizableString("医疗废物预警", H2ServiceConsts.LocalizationSourceName),
                         url: "MedicalWaste/Warning",
                         icon: "flaticon-warning-sign ",
                         requiredPermissionName: PermissionNames.Pages_Infection_MedicalWasteWarning
                         )
                    )
                ).AddItem(
                new MenuItemDefinition(
                    "机房管理",
                     new LocalizableString("机房管理", H2ServiceConsts.LocalizationSourceName),
                     url: "#/",
                     icon: "la la-building",                     
                     requiredPermissionName:PermissionNames.Pages_ServerRoom
                    ).AddItem(new MenuItemDefinition(
                        "机房信息",
                        new LocalizableString("机房信息", H2ServiceConsts.LocalizationSourceName),
                        url: "ServerRoom/ServerRoomIndex",
                        icon: "flaticon-computer"//,
                        //requiredPermissionName: PermissionNames.Pages_ServerRoom_Info
                        )
                   ).AddItem(new MenuItemDefinition(
                       "机房巡视",
                        new LocalizableString("机房巡视", H2ServiceConsts.LocalizationSourceName),
                        url: "ServerRoom/PatrolIndex",
                        icon: "flaticon-interface-9",
                        requiredPermissionName: PermissionNames.Pages_ServerRoom_Partrol
                       )
                    ).AddItem(new MenuItemDefinition(
                       "机房事件",
                        new LocalizableString("机房事件", H2ServiceConsts.LocalizationSourceName),
                        url: "ServerRoom/EventIndex",
                        icon: "flaticon-danger",
                        requiredPermissionName: PermissionNames.Pages_ServerRoom_Partrol
                       )
                   ).AddItem(new MenuItemDefinition(
                       "服务器/设备",
                        new LocalizableString("服务器/设备", H2ServiceConsts.LocalizationSourceName),
                        url: "ServerEquipment/Index",
                        icon: "flaticon-network",
                        requiredPermissionName: PermissionNames.Pages_ServerRoom_ServerEquipment
                       )
                    ).AddItem(new MenuItemDefinition(
                       "服务器监控",
                        new LocalizableString("服务器监控", H2ServiceConsts.LocalizationSourceName),
                        url: "ServerEquipment/ServerMonitoring",
                        icon: "la la-eye",
                        requiredPermissionName: PermissionNames.Pages_ServerRoom_ServerMonitoringInfo
                       )
               )
              ).AddItem(
                new MenuItemDefinition(
                    "系统管理",
                    new LocalizableString("系统管理", H2ServiceConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "flaticon-cogwheel",
                        requiredPermissionName: PermissionNames.Pages_System
                    ).AddItem(new MenuItemDefinition(
                        "用户管理",
                        new LocalizableString("用户管理", H2ServiceConsts.LocalizationSourceName),
                        url: "User/Index",
                        icon: "flaticon-users",                        
                        requiredPermissionName: PermissionNames.Pages_System_User
                        )
                   ).AddItem(new MenuItemDefinition(
                       "部门管理",
                        new LocalizableString("部门管理", H2ServiceConsts.LocalizationSourceName),
                        url: "Department/Index",
                        icon: "flaticon-network",
                        requiredPermissionName: PermissionNames.Pages_System_Department
                       )
                   ).AddItem(new MenuItemDefinition(
                       "科室关联模块",
                        new LocalizableString("科室关联模块", H2ServiceConsts.LocalizationSourceName),
                        url: "Department/RelateModuleIndex",
                        icon: "flaticon-network",
                        requiredPermissionName: PermissionNames.Pages_System_Department
                       )
                   )
                   .AddItem(new MenuItemDefinition(
                       "角色管理",
                        new LocalizableString("角色管理", H2ServiceConsts.LocalizationSourceName),
                        url: "Role/Index",
                        icon: "flaticon-profile",
                        requiredPermissionName: PermissionNames.Pages_System_Role
                       )                       
                  ).AddItem(new MenuItemDefinition(
                       "外部单位/人员",
                        new LocalizableString("外部单位/人员", H2ServiceConsts.LocalizationSourceName),
                        url: "ExternalCompany/Index",
                        icon: "flaticon-network",
                        requiredPermissionName:PermissionNames.Pages_System_ExternalCompany
                       )
                   )
                );

        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, H2ServiceConsts.LocalizationSourceName);
        }

    }
}
