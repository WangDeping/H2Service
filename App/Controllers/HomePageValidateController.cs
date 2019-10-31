using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using App.Models.HomePageValidate;
using H2Service.Authorization;
using H2Service.Extensions;
using H2Service.HomePages;
using H2Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [AbpMvcAuthorize]
    public class HomePageValidateController : AppControllerBase
    {
        private IHomePageAppService _homePageAppService;
        private IUserAppService _userAppService;
        public HomePageValidateController(IHomePageAppService homePageAppService,
           IUserAppService userAppService) {
            _homePageAppService = homePageAppService;
            _userAppService = userAppService;

        }
      
        public ActionResult ValidateMessage(int Id) {
            var msg = _homePageAppService.GetValidateMessageById(Id);
            var model = new ValidateMessageModel
            {
                Msg = msg,
                IsAdministrator = false,
                IsQCDoctor = false,
                IsSender=false,
                IsSelf=false
            };           
            if (msg != null)
            {
                if (PermissionChecker.IsGrantedAsync(PermissionNames.Pages_QC_HomPageAdministrator).Result)
                    model.IsAdministrator = true;
                if (PermissionChecker.IsGrantedAsync(PermissionNames.Pages_QC_QCDoctor).Result)
                    model.IsQCDoctor = true;
                if (msg.SendUser == AbpSession.GetUserNumber())
                    model.IsSender = true;
                if (msg.UserNumber == AbpSession.GetUserNumber())
                    model.IsSelf = true;
                model.SenderUserName =string.IsNullOrEmpty(msg.SendUser)?"System": _userAppService.GetUserByNumber(msg.SendUser).UserName;
                model.UserName = _userAppService.GetUserByNumber(msg.UserNumber).UserName;
            }
            return View(model);
        }
       
        [DontWrapResult]
        public JsonResult DeleteMessage(int Id) {         
            try
            {
                _homePageAppService.DeleteMessage(Id);
                return Json(new ErrorInfo(0, "成功"));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new ErrorInfo { Code = -1, Message = ex.Message });
            }
        }
      
        public ActionResult GetValidateMessagesList() {

            var messages = _homePageAppService.GetValidateMessages(AbpSession.GetUserNumber());
            return View(messages);
        }
      
        [DontWrapResult]
        public JsonResult CorrectThenNotify(int Id) {
            try
            {
                _homePageAppService.CorrectThenNotify(Id);
                return Json(new ErrorInfo(0, "成功"));
            }
            catch(Exception ex) {
                return Json(new ErrorInfo(-1, ex.Message));
            }
        
        }
        [DontWrapResult]
        public JsonResult Pass(int Id) {
            try
            {
                _homePageAppService.PassValidate(Id);
                return Json(new ErrorInfo(0, "成功"));
            }
            catch (Exception ex)
            {
                return Json(new ErrorInfo(-1, ex.Message));
            }

        }
        [DontWrapResult]
        public JsonResult NoPass(int Id) {
            try
            {
                _homePageAppService.NoPassValidate(Id);
                return Json(new ErrorInfo(0, "成功"));
            }
            catch (Exception ex)
            {
                return Json(new ErrorInfo(-1, ex.Message));
            }
        }
        // GET: HomePageValidate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Default() {

            return View();

        }
    }
}