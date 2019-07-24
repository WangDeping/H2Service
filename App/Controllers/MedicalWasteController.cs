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
using H2Service.Extensions;
using H2Service.Authorization;
using App.Models.Waste;

namespace App.Controllers
{
    [AbpMvcAuthorize]
    public class MedicalWasteController : AppControllerBase
    {
        private readonly WxTokenManager _wxTokenManager;
        private readonly IDistrictAppService _districtAppService;
        private readonly IUserAppService _userAppService;
        private readonly IMedicalWasteAppService _medicalWasteAppService;
        private readonly IDepartmentAppService _departmentAppService;
        private readonly WxFileManager _wxFileManager;
        private readonly string wxTempFilePath;
        public MedicalWasteController(
            WxTokenManager wxTokenManager,
            WxFileManager wxFileManager,
            IDistrictAppService districtAppService,
            IMedicalWasteAppService medicalWasteAppService,
            IDepartmentAppService departmentAppService,
            IUserAppService userAppService)
        {
            _wxTokenManager = wxTokenManager;
            _districtAppService = districtAppService;
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
        /// <summary>
        /// 根据条码号判断是否占用
        /// </summary>
        /// <param name="code">条码</param>
        /// <returns>True代表占用，False代表未被占用</returns>
        [DontWrapResult]
        public bool IsExistWasteCode(string code) {
            var medicalwaste = _medicalWasteAppService.GetWasteByCode(code);        
            return medicalwaste!=null;
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

        /// <summary>
        /// 获取未出库的医疗废物
        /// </summary>
        /// <param name="districtId">医疗废物存储位置</param>
        /// <returns></returns>
        [AbpMvcAuthorize(PermissionNames.Pages_Infection_MedicalWasteWorker)]
        public ActionResult GetUnDeliveryCollection(int Id)
        {

            var undeliveryList = _medicalWasteAppService.GetUnDeliveryCollection(Id);
            ViewBag.DistrictId = Id;
            ViewBag.DistrictName = _districtAppService.GetDistrictList().First(T => T.Id == Id).DistrictName;
            ViewBag.UserName =AbpSession.GetUserName() ;
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            return View("MedicalWasteTemporaryStorage", undeliveryList);

        }
        [AbpMvcAuthorize(PermissionNames.Pages_Infection_MedicalWasteWorker)]
        [DontWrapResult]
        public JsonResult Delivery(DeliveryCollectionRequestModel request)
        {
            try
            {
                var urls =new List<string>();
                var savePath = Server.MapPath(@"~/Content/WxTemp/MedicalWaste/");               
                foreach (var url in request.ImagesUrl) {
                  urls.Add(wxTempFilePath + "MedicalWaste/" + _wxFileManager.DownLoadWxTempFile(url, savePath));
                }
                _medicalWasteAppService.DeliveryCollection(new DeliveryCollectionInput {  DistrictId=request.DistrictId, ImagesUrl=urls});
                return Json(new ErrorInfo { Code = 1, Details = "成功", Message = "成功" });
            }
            catch (Exception ex)
            {
                return Json(new ErrorInfo { Code = -1, Details = "操作失败", Message = ex.Message });
            }
        }

        /// <summary>
        /// 出库历史
        /// </summary>
        /// <param name="Id">废物暂存点Id</param>
        /// <returns></returns>
        [AbpMvcAuthorize(PermissionNames.Pages_Infection_MedicalWasteWorker)]
        public ActionResult DeliveryHistory(int Id)
        {
            ViewBag.DistrictName = _districtAppService.GetDistrictList().First(T => T.Id == Id).DistrictName+"出库历史";
            ViewBag.DistrictId = Id;
            return View();
        }

        [AbpMvcAuthorize(PermissionNames.Pages_Infection_MedicalWasteWorker)]
        [DontWrapResult]    
        public PartialViewResult GetPagedDeliveryHistory(GetPagedDeliveryInput request)
        {
            var result = _medicalWasteAppService.GetPagedDeliveryHistory(request);          
            return PartialView("_GetPagedDeliveryHistory",new PagedDeliveryHistoryRequestModel { total =result.TotalCount, rows = result.Items });
        }
    }
}