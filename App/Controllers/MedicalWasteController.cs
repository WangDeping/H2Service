using Abp.Web.Models;
using H2Service.External;
using H2Service.MedicalWastes;
using H2Service.MedicalWastes.Dto;
using H2Service.WxWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using H2Service.Account;
using App.Models;
using System.Configuration;
using Abp.UI;
using H2Service.Users;

namespace App.Controllers
{
    [AbpMvcAuthorize]
    public class MedicalWasteController : AppControllerBase
    {
        private readonly WxTokenManager _wxTokenManager;
        private readonly IExternalUserAppService _externalUserAppService;
        private readonly IUserAppService _userAppService;
        private readonly IMedicalWasteAppService _medicalWasteAppService;
        private readonly IDepartmentAppService _departmentAppService;
        private readonly WxFileManager _wxFileManager;
        private readonly string wxTempFilePath;
        public MedicalWasteController(
            WxTokenManager wxTokenManager,
            WxFileManager wxFileManager,
            IExternalUserAppService externalUserAppService,
            IMedicalWasteAppService medicalWasteAppService,
            IDepartmentAppService departmentAppService,
            IUserAppService userAppService)
        {
            _wxTokenManager = wxTokenManager;
            _externalUserAppService = externalUserAppService;
            _medicalWasteAppService = medicalWasteAppService;
            _departmentAppService = departmentAppService;
            _userAppService = userAppService;
            wxTempFilePath = ConfigurationManager.AppSettings["wxTempFilePath"];
            _wxFileManager = wxFileManager;
           
        }
       
        public ActionResult Index(int Id)
        {
            if (_departmentAppService.GetById(Id) == null)
                throw new UserFriendlyException("非法科室");
            var flow = _medicalWasteAppService.GetFlowByDepartment(Id);
            ViewBag.Flow = flow;
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            return View();
        }

        public PartialViewResult WasteGroup(int Id)
        {
            var wasteGroup = _medicalWasteAppService.GetMedicalWasteByFlowId(Id);
            ViewBag.FlowId = wasteGroup.FlowId;
            return PartialView("_WasteGroupByDepartment", wasteGroup);
        }

        public PartialViewResult GetImages(int Id)
        {
            var images = _medicalWasteAppService.GetImages(Id);
            return PartialView("_GetImages", images);
        }

        public ActionResult AppendWaste(AppendWasteInput input)
        {
            _medicalWasteAppService.AppendWaste(input);
            var wasteGroup = _medicalWasteAppService.GetMedicalWasteByFlowId(input.FlowId);
            return PartialView("_WasteGroupByDepartment", wasteGroup);
         

        }
        [DontWrapResult]
        public JsonResult AppendImage(AppendWasteImageModel request)
        {
            try
            {
                var savePath = Server.MapPath(@"~/Content/WxTemp/MedicalWaste/");
                var url = wxTempFilePath + "MedicalWaste/" + _wxFileManager.DownLoadWxTempFile(request.ServerId, savePath);
                var imageDto = new WasteImageDto { ImgPath = url, Status = MedicalWasteStatus.科室点, FlowId = request.FlowId };
               
                _medicalWasteAppService.AppendImage(imageDto);
                return Json(new ErrorInfo(0, "成功"));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Json(new ErrorInfo(-1, "失败"));

            }
        }

        [DontWrapResult]
        public JsonResult GetUser(string code)
        {
            try
            {
                var user = _userAppService.GetUserByCode(code);
                user.AvatarUrl = @WebConfigurationManager.AppSettings["mainWebUrl"] + @"Content/avatars/" + user.UserNumber + ".jpg";
                return Json(user, JsonRequestBehavior.AllowGet);
            }
            catch (UserFriendlyException ex)
            {
                return Json(new ErrorInfo {  Code=-1, Message=ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
       
        public ActionResult PreviewFlow (SetCollectUserModel request)
        {           
            _medicalWasteAppService.SetCollectUser(request.FlowId,request.CollectUserId);
            var previewFlow = _medicalWasteAppService.GetMedicalWasteByFlowId(request.FlowId);
            return View(previewFlow);
        }
        [DontWrapResult]
        public JsonResult LeaveFromDepartment(int flowId)
        {
            _medicalWasteAppService.LeaveFromDepartment(flowId);
            return Json(new ErrorInfo(0, "成功"));
        }


        public ActionResult WasteList(WasteListRequestModel request)
        {
            ViewBag.Kind = request.Kind;
            var output = _medicalWasteAppService.GetMedicalWasteByFlowId(request.FlowId);
            var wasteList= output.DetailWasteList.Where(T=>T.Kind==request.Kind);
            ViewBag.DepId =output.Flow.DepartmentId;
            return View(wasteList);
            
        }
        [DontWrapResult]
        public JsonResult RemoveWaste(int Id)
        {
            try
            {
                _medicalWasteAppService.RemoveWaste(Id);
                return Json(new ErrorInfo(0, "成功"));
            }
            catch (UserFriendlyException ex) {
                return Json(new ErrorInfo { Code =- 1, Message=ex.Message });
            }
           
        }
    }
}