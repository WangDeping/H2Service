using Abp.AutoMapper;
using Abp.Modules;
using Abp.Runtime.Caching;
using Abp.Runtime.Caching.Redis;
using H2Service.Account.Dto;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using H2Service.EnumDic;
using H2Service.Equipments;
using H2Service.Equipments.Dto;
using H2Service.External.Dto;
using H2Service.H2Modules;
using H2Service.HomePages.Dto;
using H2Service.MedicalData.HomePages;
using H2Service.MedicalData.OPMedicalDiagnose;
using H2Service.MedicalWastes;
using H2Service.MedicalWastes.Dto;
using H2Service.MeritPays;
using H2Service.MeritPays.Dto;
using H2Service.QC;
using H2Service.QC.Dto;
using H2Service.Reports.Dto;
using H2Service.Salaries.Dto;
using H2Service.Salarires;
using H2Service.Scheduling;
using H2Service.Scheduling.Dto;
using H2Service.ServerRooms;
using H2Service.ServerRooms.Dto;
using H2Service.SMS;
using H2Service.Users.Dto;
using H2Service.WxWork.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace H2Service
{
    [DependsOn(typeof(H2ServiceCoreModule), 
        typeof(AbpAutoMapperModule),
         typeof(H2ServiceWxWorkModule),
         typeof(H2ServiceSMSModule)
        )]
    public class H2ServiceApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
            IocManager.Register<ICacheManager, AbpRedisCacheManager>();

        }
        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
           
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {

                cfg.CreateMap<SalaryPeriod, SalaryPeriodDto>();
                cfg.CreateMap<ServerRoom, ServerRoomDto>()
                .ForMember(d => d.DepartmentName, opt =>opt.MapFrom(s => s.Department.DepartmentName));
                cfg.CreateMap<ServerRoomPatrol, ServerRoomPatrolDto>()
                .ForMember(d => d.CreatorName, opt =>
                {
                    opt.MapFrom(s => s.CreatorUser.UserName);
                })
                 .ForMember(d => d.ServerRoomName, opt =>
                 {
                     opt.MapFrom(s => s.ServerRoom.RoomName);
                 })
                 .ForMember(d => d.CreationTime, opt =>
                 {
                     opt.MapFrom(s => s.CreationTime.ToString("yyyy-MM-dd HH:mm"));
                 });

                cfg.CreateMap<ServerEquipment, ServerEquipmentDto>()
                .ForMember(d => d.ServerRoomName, opt => opt.MapFrom(s => s.ServerRoom.RoomName))
                .ForMember(d => d.OperatingSystem, opt => opt.MapFrom(s => Enum.GetName(typeof(ServerRooms.OperatingSystem), s.OperatingSystem.Value)))
                .ForMember(d => d.SEType, opt => opt.MapFrom(s => Enum.GetName(typeof(SEType), s.SEType.Value)))
                .ForMember(d => d.PurchaseDate, opt => opt.MapFrom(s => s.PurchaseDate != null ? s.PurchaseDate.Value.ToString("yyyy-MM-dd") : ""));

                cfg.CreateMap<User, UpdateUserInput>()
                 .ForMember(d => d.Gender, opt => opt.MapFrom(s => (int)s.Gender));

                cfg.CreateMap<QCAppraisalPeriod, QCAppraisalPeriodDto>()
                .ForMember(d => d.CreatorUserName, opt => opt.MapFrom(s => s.CreatorUser.UserName))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enum.GetName(typeof(QCAppraisalPeriodStatus), s.Status)))
                .ForMember(d => d.CreationTime, opt => { opt.MapFrom(s => s.CreationTime.ToString("yyyy-MM-dd")); })
                .ForMember(d => d.BeginDate, opt => { opt.MapFrom(s => s.BeginDate.ToString("yyyy-MM-dd")); })
                .ForMember(d => d.EndDate, opt => { opt.MapFrom(s => s.EndDate.ToString("yyyy-MM-dd")); })
                .ForMember(d => d.CreatorDepartmentName, opt => opt.MapFrom(s => s.CreatorDepartment.DepartmentName));

                cfg.CreateMap<QCAppraisalDetail, QCDetailDto>()
                .ForMember(d => d.CreatorUserName, opt => opt.MapFrom(s => s.CreatorUser.UserName))
                .ForMember(d => d.PunishedDepartmentName, opt => opt.MapFrom(s => s.PunishedDepartment.DepartmentName))
                .ForMember(d => d.FunctionalDepartmentName, opt => opt.MapFrom(s => s.FunctionalDepartment.DepartmentName))
                .ForMember(d => d.PunishedUserName, opt => opt.MapFrom(s => s.PunishedUser.UserName));

                cfg.CreateMap<MedicalWasteFlow, WasteFlowDto>()
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.DepartmentName))
                .ForMember(d => d.CollectUserName, opt => opt.MapFrom(s => s.CollectUser.UserName))
                .ForMember(d => d.NurseName, opt => opt.MapFrom(s => s.Nurse.UserName))
                .ForMember(d => d.CollectTime, opt => { opt.MapFrom(s => s.CollectTime == null ? "" : s.CollectTime.Value.ToString("yyyy-MM-dd HH:mm:ss")); })
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enum.GetName(typeof(MedicalWasteStatus), s.Status)));

                cfg.CreateMap<MedicalWasteFlow, GetHandOverFlowListDto>()
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.DepartmentName))
                .ForMember(d => d.District, opt => opt.MapFrom(s => s.Department.District.DistrictName))
                .ForMember(d => d.CollectUserName, opt => opt.MapFrom(s => s.CollectUser.UserName))
                .ForMember(d => d.NurseName, opt => opt.MapFrom(s => s.Nurse.UserName))
                .ForMember(d => d.CollectTime, opt => { opt.MapFrom(s => s.CollectTime == null ? "" : s.CollectTime.Value.ToString("yyyy-MM-dd HH:mm:ss")); })
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enum.GetName(typeof(MedicalWasteStatus), s.Status)))                 
                .ForMember(d => d.Chemical, opt => opt.MapFrom(s =>s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.化学性废物).Sum(T => T.Weight)+"Kg/"+ s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.化学性废物).Count()+ "包"))
                .ForMember(d => d.Damage, opt => opt.MapFrom(s => s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.损伤性废物).Sum(T => T.Weight) + "Kg/" + s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.损伤性废物).Count() + "包"))
                .ForMember(d => d.Infectious, opt => opt.MapFrom(s => s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.感染性废物).Sum(T => T.Weight)+"Kg/" + s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.感染性废物).Count() + "包"))
                .ForMember(d => d.Pathology, opt => opt.MapFrom(s => s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.病理性废物).Sum(T => T.Weight)+"Kg/" + s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.病理性废物).Count() + "包"))
                .ForMember(d => d.Pharmaceutical, opt => opt.MapFrom(s => s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.药学性废物).Sum(T => T.Weight)+"Kg/" + s.MedicalWasteCollection.Where(T => T.Kind == MedicalWasteKind.药学性废物).Count() + "包"));

                cfg.CreateMap<MedicalWaste, MedicalWasteDto>()
                .ForMember(d => d.CreatorUserName, opt => opt.MapFrom(s => s.CreatorUser.UserName));

                cfg.CreateMap<MedicalWasteDelivery, WasteDeliveryDto>()
                .ForMember(d => d.CreatorUserName, opt => opt.MapFrom(s => s.CreatorUser.UserName))
                .ForMember(d => d.DistrictName, opt => opt.MapFrom(s => s.District.DistrictName))
                .ForMember(d => d.CreationTime, opt => opt.MapFrom(s => s.CreationTime.ToString("yyyy-MM-dd HH:mm")));


                cfg.CreateMap<DepartmentRelateModule, DepartmentModulesRelationDto>()
                .ForMember(d=>d.DepartmentName,opt=>opt.MapFrom(s=>s.Department.DepartmentName));

                cfg.CreateMap<ExternalUser, ExternalUserDto>()
                .ForMember(d => d.ExternalCompanyName, opt => opt.MapFrom(s => s.Company.CompanyName))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => Enum.GetName(typeof(Gender), s.Gender)));

                cfg.CreateMap<ExternalUser, GetExternalUserByCodeOutput>()
               .ForMember(d => d.ExternalCompanyName, opt => opt.MapFrom(s => s.Company.CompanyName))
               .ForMember(d => d.Gender, opt => opt.MapFrom(s => Enum.GetName(typeof(Gender), s.Gender)));

                cfg.CreateMap<MeritPayPeriod, MeritPayPeriodDto>()
                .ForMember(d => d.CreationTime, opt => opt.MapFrom(s => s.CreationTime.ToString("yyyy-MM-dd hh:mm")))
                .ForMember(d => d.CreatorName,opt => opt.MapFrom(s => s.Creator.UserName));




                cfg.CreateMap<WxGetUserInfo, CreateUserDto>()
                .ForMember(d => d.AvatarUrl, opt => { opt.MapFrom(s => s.avatar); })
                .ForMember(d => d.Email, opt => { opt.MapFrom(s => s.email); })
                .ForMember(d => d.TelPhone, opt => { opt.MapFrom(s => s.mobile); })
                .ForMember(d => d.Gender, opt => { opt.MapFrom(s => s.gender); })
                .ForMember(d => d.UserName, opt => { opt.MapFrom(s => s.name); })
                .ForMember(d => d.UserNumber, opt => { opt.MapFrom(s => s.userid); });

                cfg.CreateMap<SchedulingDepartmentUser, SchedulingDepartmentUserDto>()
                 .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.DepartmentName))
                 .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.UserName))
                 .ForMember(d => d.UserNumber, opt => opt.MapFrom(s => s.User.UserNumber));

                cfg.CreateMap<H2ModuleWithAuditing,H2ModuleWithAuditingDto>()
                 .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.DepartmentName))
                 .ForMember(d => d.DoUserName, opt => opt.MapFrom(s => s.DoUser.UserName))
                 .ForMember(d => d.AuditorName, opt => opt.MapFrom(s => s.Auditor.UserName));

                cfg.CreateMap<HomePageValidateMessage, HomePageValidateMessageDto>()
                .ForMember(d=>d.DischargeDate,opt=>opt.MapFrom(s=>s.DischargeDate==null?"":s.DischargeDate.Value.ToShortDateString()))
                .ForMember(d=>d.SendTime,opt=>opt.MapFrom(s=>s.SendTime.ToString()))
                .ForMember(d => d.ValidateType, opt => opt.MapFrom(s => Enum.GetName(typeof(ValidateType), s.ValidateType)));

                cfg.CreateMap<OPMedicalDiagnose, OPQtyMonthDto>()
                .ForMember(d => d.Month, opt => opt.MapFrom(s => s.AdDate.Value.ToString("yyyy-MM")))
                .ForMember(d => d.Dep, opt => opt.MapFrom(s => s.AdmDep));


                cfg.CreateMap<Equipment, EquipmentOutput>()
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Dept.DepartmentName))
                .ForMember(d => d.EquipmentKindName, opt => opt.MapFrom(s => s.Kind.EquipmentKindName))
                .ForMember(d => d.EquipmentTypeName, opt => opt.MapFrom(s => s.Type.EquipmentTypeName))
                 .ForMember(d => d.EquipmentTypeIcon, opt => opt.MapFrom(s => s.Type.Icon))
                .ForMember(d => d.EquipmentModelName, opt => opt.MapFrom(s => s.Model.EquipmentModelName))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enum.GetName(typeof(EquipmentStatus), s.Status)))
                .ForMember(d=>d.IsUsing,opt=>opt.MapFrom(s=>s.UsageLogs.FirstOrDefault(T=>T.EndUserId==null)!=null))
                .ForMember(d => d.IsLoaning_Id, opt => opt.MapFrom(s => s.LoanLogs.FirstOrDefault(T => T.SignUserId == null).Id))
                .ForMember(d => d.HasPatrol_I, opt => opt.MapFrom(s => s.PatrolLogs.Any(T => T.EquipmentId==s.Id&&T.CreationTime>=DateTime.Now.Date&&T.Type==PatrolTypeEnum.I级巡视)))
                .ForMember(d => d.HasPatrol_II, opt => opt.MapFrom(s => s.PatrolLogs.Any(T=>T.CreationTime>= DateTime.Now.AddDays(1 - DateTime.Now.Day).Date&&T.EquipmentId==s.Id&&T.Type==PatrolTypeEnum.II级巡视)));

                cfg.CreateMap<EquipmentProperty, EquipmentPropertyDto>()
                .ForMember(d => d.Frequency, opt => opt.MapFrom(s => Enum.GetName(typeof(EquipmentPartrolFrequencyEnum), s.Frequency)))
                .ForMember(d => d.ViewType, opt => opt.MapFrom(s => Enum.GetName(typeof(ViewType), s.ViewType)))
                 .ForMember(d => d.PartrolType, opt => opt.MapFrom(s => Enum.GetName(typeof(PatrolTypeEnum), s.PartrolType)));

                cfg.CreateMap<EquipmentLoanLog, EquipmentLoanLogDto>()
                .ForMember(d => d.BorrowDeptName, opt => opt.MapFrom(s => s.BorrowDepartment.DepartmentName))
                .ForMember(d => d.BorrowUserName, opt => opt.MapFrom(s => s.BorrowUser.UserName))
                .ForMember(d => d.LoanDepartmentName, opt => opt.MapFrom(s => s.LoanDepartment.DepartmentName))
                .ForMember(d => d.LoanUserName, opt => opt.MapFrom(s => s.LoanUser.UserName))
                .ForMember(d => d.SignUserName, opt => opt.MapFrom(s => s.SignUser.UserName))
                .ForMember(d => d.ReturnUserName, opt => opt.MapFrom(s => s.ReturnUser.UserName));

                cfg.CreateMap<EquipmentPatrolLog, EquipmentPatrolLogDto>()
                .ForMember(d => d.CreatorUserName, opt => opt.MapFrom(s => s.CreatorUser.UserName))
                .ForMember(d => d.EquipmentName, opt => opt.MapFrom(s => s.Equipment.EquipmentName))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => Enum.GetName(typeof(PatrolTypeEnum), s.Type)))
                .ForMember(d=>d.EquipmentTypeName,opt=>opt.MapFrom(s=>s.Equipment.Type.EquipmentTypeName))
                .ForMember(d=>d.Code,opt=>opt.MapFrom(s=>s.Equipment.Code));

            });

        }

    }
}
