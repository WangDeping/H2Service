using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using H2Service.Account;
using H2Service.Account.Dto;
using H2Service.Authorization.Departments;
using H2Service.Helpers;
using H2Service.MedicalWastes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
    public class MedicalWasteAppService : H2ServiceAppServiceBase, IMedicalWasteAppService
    {
        private IRepository<MedicalWaste> _medicalWasteRepository;
        private IRepository<MedicalWasteFlow> _flowRepository;
        private IRepository<MedicalWasteDelivery> _deliveryRepository;
        private readonly IMedicalWasteDomainService _medicalWasteDomainService;
        private readonly IDepartmentAppService _departmentAppService;
        public new IAbpSession AbpSession { get; set; }
        public MedicalWasteAppService(IRepository<MedicalWaste> medicalWasteRepository,
            IRepository<MedicalWasteFlow> flowRepository,
            IRepository<MedicalWasteDelivery> deliveryRepository,
            IMedicalWasteDomainService medicalWasteDomainService,
            IDepartmentAppService departmentAppService
            )
        {
            _medicalWasteRepository = medicalWasteRepository;
            _flowRepository = flowRepository;
            AbpSession = NullAbpSession.Instance;
            _deliveryRepository = deliveryRepository;
            _medicalWasteDomainService = medicalWasteDomainService;
            _departmentAppService = departmentAppService;

        }

        /// <summary>
        /// 获取科室暂存点的分类医疗废物，若是么有则初始化一个
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public WasteFlowDto GetFlowByDepartment(int departmentId)
        {
            return _medicalWasteDomainService.InitOrGetDefaultFlow(departmentId).MapTo<WasteFlowDto>();          

        }

        public DisplayWasteInDepartmentOutput GetMedicalWasteByFlowId(int flowId)
        {
            var ret = new DisplayWasteInDepartmentOutput();
            var flow = _flowRepository.Get(flowId);
            ret.FlowId = flowId;
            var wasteCollection = flow.MedicalWasteCollection;
            ret.DetailWasteList = wasteCollection.OrderByDescending(T => T.Id).MapTo<List<MedicalWasteDto>>();
            var wasteGroup = flow.MedicalWasteCollection
             .GroupBy(T => T.Kind).Select(g => new GroupWasteDto
             {
                 Count = g.Count(),
                 Kind = (int)g.Key,
                 KindName = Enum.GetName(typeof(MedicalWasteKind), g.Key),
                 TotalWeight = g.Sum(item => item.Weight)
             }).ToList();

            foreach (MedicalWasteKind kind in Enum.GetValues(typeof(MedicalWasteKind)))
            {
                if (wasteGroup.FirstOrDefault(T => T.Kind == (int)kind) == null)
                    wasteGroup.Add(new GroupWasteDto
                    {
                        Count = 0,
                        Kind = (int)kind,
                        TotalWeight = 0,
                        KindName = Enum.GetName(typeof(MedicalWasteKind), kind)
                    });
            }
            ret.Flow = flow.MapTo<WasteFlowDto>();
            ret.GroupWasteList = wasteGroup.OrderBy(T => T.Kind).ToList();
            ret.Images = flow.ImagesCollection.MapTo<List<WasteImageDto>>();
            return ret;
        }

        /// <summary>
        /// 交接记录表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GetHandOverFlowListDto> GetHandOverFlowList(GetHandOverFlowListInput input)
        {
             input.End = input.End.AddDays(1);            
            var query = _flowRepository.GetAll().
                WhereIf(input.DepartmentId != 0, T => T.DepartmentId == input.DepartmentId)
                .Where(T=>T.Status!=MedicalWasteStatus.科室点)
                .Where(T => (T.CollectTime >= input.Start && T.CollectTime <= input.End))
                .OrderByDescending(T => T.CollectTime);
            return query.ToList().MapTo<List<GetHandOverFlowListDto>>();
         
           
        }     

        public IEnumerable<WasteStatisticOutput> WasteStatistic(WasteStatisticInput input)
        {
                       
            var wasteStatisticOutput = _medicalWasteRepository.GetAll()
                .WhereIf(input.Status==MedicalWasteStatus.科室点, T => T.CreationTime > input.Start && T.CreationTime < input.End&&T.Flow.Status==input.Status)
                 .WhereIf(input.Status == MedicalWasteStatus.医院暂存点, T => T.Flow.CollectTime> input.Start && T.Flow.CollectTime < input.End &&T.Flow.Status == input.Status)
                 // .WhereIf(input.Status == MedicalWasteStatus.卫康公司, T => T.Flow.DeliveryTime> input.Start && T.Flow.DeliveryTime < input.End &&T.Flow.Status == input.Status)
                .WhereIf(input.DepartmentId > 0, T => T.Flow.DepartmentId == input.DepartmentId)
               // .Where(T=>T.Flow.Status==input.Status)
                .GroupBy(T => T.Kind).Select(g => new WasteStatisticOutput { Kind = g.Key, Total = g.Sum(item => item.Weight),PackageCount=g.Count() })
                .ToList();
            foreach (MedicalWasteKind kind in Enum.GetValues(typeof(MedicalWasteKind)))
            {
                if (!wasteStatisticOutput.Any(T=>T.Kind==kind))
                    wasteStatisticOutput.Add(new WasteStatisticOutput { Kind = kind, Total = 0 });
            }
            return wasteStatisticOutput;

        }

        /// <summary>
        /// 待出库垃圾汇总
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public IEnumerable<WasteStatisticOutput> GetUnDeliveryCollection(int districtId)
        {
            var wasteStatisticOutput = _medicalWasteRepository.GetAll()
                .Where(T=>T.Flow.Status==MedicalWasteStatus.医院暂存点&&T.Flow.Department.DistrictId==districtId)
               .GroupBy(T => T.Kind).Select(g => new WasteStatisticOutput { Kind = g.Key, Total = g.Sum(item => item.Weight), PackageCount = g.Count() })
               .ToList();
            foreach (MedicalWasteKind kind in Enum.GetValues(typeof(MedicalWasteKind)))
            {
                if (!wasteStatisticOutput.Any(T => T.Kind == kind))
                    wasteStatisticOutput.Add(new WasteStatisticOutput { Kind = kind, Total = 0 });
            }
            return wasteStatisticOutput;
        }

        public void DeliveryCollection(int districtId)
        {
            IEnumerable<WasteStatisticOutput> wasteCollection = this.GetUnDeliveryCollection(districtId);
            string summary = "";
            foreach (var w in wasteCollection)
                summary +=w.Kind+":"+w.Total+"Kg"+","+w.PackageCount+"包;";
            _medicalWasteDomainService.Delivery(districtId,summary);
        }

        public IList<WasteDeliveryDto> GetDeliveryHistory(int districtId)
        {
            var list = _deliveryRepository.GetAll().Where(T => T.DistrictId == districtId);          
            return list.MapTo<List<WasteDeliveryDto>>();
        }

        public void AppendWaste(AppendWasteInput input)
        {
            if (input.Weight <= 0)
                return;
            var waste = input.MapTo<MedicalWaste>();
            var id = _medicalWasteRepository.InsertAndGetId(waste);
            waste.Code = id.ToString("D7")+new Random().Next(100,999);
        }

        public void AppendImage(WasteImageDto dto)
        {
            var flow = _flowRepository.Get(dto.FlowId);
            flow.ImagesCollection.Add(dto.MapTo<MedicalWasteImage>());

        }

        public List<WasteImageDto> GetImages(int flowId)
        {
            var flow = _flowRepository.Get(flowId);
            var images = flow.ImagesCollection.ToList();
            return images.MapTo<List<WasteImageDto>>();

        }
        [Abp.Authorization.AbpAuthorize]
        public void SetCollectUser(int flowId, long collectUserId)
        {
            var flow = _flowRepository.Get(flowId);
            flow.NurseId = AbpSession.GetUserId();
            flow.CollectTime = DateTime.Now;
            flow.CollectUserId = collectUserId;
        }

        public void LeaveFromDepartment(int flowId)
        {
            var flow = _flowRepository.Get(flowId);
            flow.Status = MedicalWasteStatus.医院暂存点;
        }
        /// <summary>
        /// 删除医疗废物包(仅限本人操作未出科室的医疗废物)
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveWaste(int Id)
        {
            var waste = _medicalWasteRepository.FirstOrDefault(Id);
            if(waste==null)
                throw new UserFriendlyException("已经删除");
            if (waste.Flow.Status > MedicalWasteStatus.科室点)
                throw new UserFriendlyException("禁止操作");
            if (AbpSession.GetUserId() != waste.CreatorUserId)
                throw new UserFriendlyException("非称重人操作");
            _medicalWasteRepository.Delete(Id);
        }


        /// <summary>
        /// 查询近期几天内无医疗废物产生科室
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public IEnumerable<DepartmentDto> GetDepartmentsWhoDontHaveWaste(int days)
        {
            var date = DateTime.Now.AddDays(0 - days);
            var shouleHaveList = _departmentAppService.GetRelatedDepartments((int)H2Module.医疗废物).Select(T=>
                    new DepartmentDto {  Id=T.DepartmentId, DepartmentName=T.DepartmentName}).ToList();
            var hasList = _medicalWasteRepository.GetAll().Where(T => T.CreationTime >= date && T.CreationTime <= DateTime.Now)
                .Select(T=>new DepartmentDto { Id=T.Flow.DepartmentId, DepartmentName=T.Flow.Department.DepartmentName})
                .Distinct().ToList();
            var expectList=shouleHaveList.Except(hasList,new DepartmentsListEquality());
            return expectList;

        }
        /// <summary>
        /// 查询近期几天内无医疗废物交接科室
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public IEnumerable<DepartmentDto> GetDepartmentsWhoDontHandoverWaste(int days)
        {
            var date = DateTime.Now.AddDays(0 - days);
            var shouleHaveList = _departmentAppService.GetRelatedDepartments((int)H2Module.医疗废物).Select(T =>
                    new DepartmentDto { Id = T.DepartmentId, DepartmentName = T.DepartmentName }).ToList();
            var hasList = _flowRepository.GetAll()
                .Where(T => T.Status == MedicalWasteStatus.医院暂存点)
                .GroupBy(T => new { DepartmentId = T.DepartmentId, DepartmentName = T.Department.DepartmentName })
                .Select(T => new 
                {
                    DepartmentId = T.Key.DepartmentId,
                    DepartmentName = T.Key.DepartmentName,
                    CollectTime = T.Max(x => x.CollectTime)
                })
                .Where(T=>T.CollectTime>=date)
                .Select(T => new DepartmentDto { Id = T.DepartmentId, DepartmentName = T.DepartmentName })
                .ToList();          
            var expectList = shouleHaveList.Except(hasList, new DepartmentsListEquality());
            return expectList;

        }
    }
}
