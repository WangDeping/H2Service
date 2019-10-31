using Abp.Web.Models;
using H2Service.Equipments;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class EquipmentController : H2ServiceControllerBase
    {
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly IEquipmentKindTypeAppService _typeKindAppService;

        public EquipmentController(IEquipmentAppService equipmentAppService, IEquipmentKindTypeAppService typeKindAppService) {
            _equipmentAppService = equipmentAppService;
            _typeKindAppService = typeKindAppService;


        }
        // GET: Equipment
        public ActionResult Index()
        {
            ViewBag.Types = _typeKindAppService.GetEqTypeList();
            ViewBag.Kinds = _typeKindAppService.GetEqKindList();
            ViewBag.Models = _typeKindAppService.GetEqModelList();
            return View();
        }
        public JsonResult AddorUpdateEquipment(CreateEquipmentInput input) {
            if (input.Id == 0)
                _equipmentAppService.CreateEquipment(input);
            else
                _equipmentAppService.UpdateEquipment(input);
            return Json(new ErrorInfo(0, "保存成功"));
        }      
        [DontWrapResult]
        public JsonResult EquipmentGrid(GetEquipmentInput request)
        {
            var result = _equipmentAppService.GetPagedEquipment(request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }
    }
}