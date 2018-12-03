using Abp.Domain.Repositories;
using Abp.Domain.Services;
using H2Service.Authorization.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
 public   class MedicalWasteDomainService: DomainService,IMedicalWasteDomainService
    {
        private readonly IRepository<MedicalWasteDelivery> _medicalWasteDeliveryRepository;
        private readonly IRepository<MedicalWaste> _medicalWasteRepository;
        private readonly IRepository<MedicalWasteFlow> _medicalWasteFlowRepository;
        private readonly IRepository<MedicalWasteImage> _medicalWasteImageRepository;

        public MedicalWasteDomainService(
            IRepository<MedicalWasteDelivery> medicalWasteDeliveryRepository,
            IRepository<MedicalWaste> medicalWasteRepository,
           IRepository<MedicalWasteFlow> medicalWasteFlowRepository,
           IRepository<MedicalWasteImage> medicalWasteImageRepository)
        {
            _medicalWasteDeliveryRepository = medicalWasteDeliveryRepository;
            _medicalWasteFlowRepository = medicalWasteFlowRepository;
            _medicalWasteImageRepository = medicalWasteImageRepository;
            _medicalWasteRepository = medicalWasteRepository;
        }
        /// <summary>
        /// 初始化或获取未交接的Flow
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public MedicalWasteFlow InitOrGetDefaultFlow(int departmentId)
        {
            var flow = _medicalWasteFlowRepository.FirstOrDefault(T => T.DepartmentId == departmentId && T.Status == MedicalWasteStatus.科室点);
            if (flow == null)
            {
                var id = _medicalWasteFlowRepository.InsertAndGetId(new MedicalWasteFlow { Status = MedicalWasteStatus.科室点, DepartmentId = departmentId });
                return _medicalWasteFlowRepository.Get(id);
            }
            else
                return flow;

        }

        /// <summary>
        /// 医疗垃圾在暂存点出库(变更Flow状态)
        /// </summary>
        /// <param name="districtId">院区Id</param>
        public void Delivery(int districtId,string summary)
        {
            var unDeliveryFlowList = _medicalWasteFlowRepository.GetAll().Where(T => T.Department.DistrictId == districtId && T.Status == MedicalWasteStatus.医院暂存点).ToList();
            if (unDeliveryFlowList.Count() == 0)
                return;  
            var delivery = new MedicalWasteDelivery { DistrictId = districtId, Summary=summary };
            _medicalWasteDeliveryRepository.InsertAndGetId(delivery);
            foreach (var flow in unDeliveryFlowList)
                flow.Status = MedicalWasteStatus.卫康公司;
            delivery.MedicalWasteFlowCollection = unDeliveryFlowList;
        }



        
    }
}
