using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
    public class MedicalWasteManger : DomainService
    {
        private IRepository<MedicalWaste> _medicalWasteRepository;
        private IRepository<MedicalWasteFlow> _flowRepository;
        public MedicalWasteManger(IRepository<MedicalWaste> medicalWasteRepository,
            IRepository<MedicalWasteFlow> flowRepository)
        {
            _medicalWasteRepository = medicalWasteRepository;
            _flowRepository = flowRepository;

        }

        //public void InitDepartmentWaste(int departmentId)
        //{
        //    var flow = _flowRepository.FirstOrDefault(T => T.DepartmentId == departmentId && T.Status == MedicalWasteStatus.科室暂存点);
        //    if (flow == null)
        //    {
        //        flow = new MedicalWasteFlow { Status = MedicalWasteStatus.科室暂存点, DepartmentId = departmentId };
        //        _flowRepository.InsertAndGetId(flow);
        //    }
        //    foreach (MedicalWasteKind kind in Enum.GetValues(typeof(MedicalWasteKind)))
        //    {
        //        if (flow.MedicalWasteCollection.FirstOrDefault(T => T.Kind == kind) == null)
        //            flow.MedicalWasteCollection.Add(new MedicalWasteInDepartmentOutput
        //            {
        //                Count = 0,
        //                Kind = (int)kind,
        //                DeartmentId = departmentId,
        //                TotalWeight = 0,
        //                KindName = Enum.GetName(typeof(MedicalWasteKind), kind)
        //            });
        //    }



        //}
    }
}
