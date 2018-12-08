using Abp.AutoMapper;
using Abp.Modules;
using Abp.Runtime.Caching;
using Abp.Runtime.Caching.Redis;
using H2Service.Account.Dto;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using H2Service.Authorization.Dto;
using H2Service.External.Dto;
using H2Service.MedicalWastes;
using H2Service.MedicalWastes.Dto;
using H2Service.MeritPays;
using H2Service.MeritPays.Dto;
using H2Service.QC;
using H2Service.QC.Dto;
using H2Service.Salaries.Dto;
using H2Service.Salarires;
using H2Service.ServerRooms;
using H2Service.ServerRooms.Dto;
using H2Service.Users.Dto;
using H2Service.WxWork.Dto;
using H2Service.WxWork.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace H2Service
{
    [DependsOn(typeof(H2ServiceCoreModule), 
        typeof(AbpAutoMapperModule),
         typeof(H2ServiceWxWorkModule)
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
                .ForMember(d => d.CreationTime, opt => opt.MapFrom(s => s.CreationTime.ToString("yyyy-MM-dd hh:mm")));


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
            });

        }

    }
}
