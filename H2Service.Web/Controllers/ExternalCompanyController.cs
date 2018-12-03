using Abp.UI;
using Abp.Web.Models;
using H2Service.Dto;
using H2Service.External;
using H2Service.External.Dto;
using H2Service.Helpers;
using H2Service.MedicalWastes;
using H2Service.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class ExternalCompanyController : H2ServiceControllerBase
    {
        private readonly IExternalCompanyAppService _externalCompanyAppService;
        private readonly IExternalUserAppService _externalUserAppService;
        public ExternalCompanyController(IExternalCompanyAppService externalCompanyAppService,
            IExternalUserAppService externalUserAppService)
        {
             _externalCompanyAppService = externalCompanyAppService;
            _externalUserAppService = externalUserAppService;

        }

        public ActionResult Index()
        {
            return View();
        }
        [DontWrapResult]
        public JsonResult CompaniesGrid(PagedInputDto request)
        {
            var result = _externalCompanyAppService.GetPagedExternalCompanies(request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ExternalCompanyAspects()
        {
            string[] allAspects = Enum.GetNames(typeof(Aspect));
            return PartialView("_PartialExternalCompanyAspects", allAspects);
        }

        public JsonResult AddOrUpdateCompany(ExternalCompanyDto request)
        {
            if (request.Id == 0)
                _externalCompanyAppService.CreateExternalCompany(request);
            else
                _externalCompanyAppService.UpdateExternalCompany(request);
            return Json(new ErrorInfo(0, "保存成功"));
        }

        [DontWrapResult]
        public JsonResult GetCompany(int Id)
        {
            var company = _externalCompanyAppService.GetExternalCompany(Id);
            return Json(company, JsonRequestBehavior.AllowGet);
        }

        [DontWrapResult]
        public JsonResult UsersGrid(GetExternalUsersInput request)
        {
            var result = _externalUserAppService.GetPagedExternalUsers(request);

            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }
        [DontWrapResult]
        public ActionResult UserQrcode(int Id)
        {
            var code = _externalUserAppService.GetCode(Id);
            var stream = QrCodeHelper.CommonQrCode(code);
            return File(stream.ToArray(), "image/jpeg");

        }
        public JsonResult AddOrUpdateUser(ExternalUserDto input)
        {
            if (input.Id == 0)
                _externalUserAppService.CreateUser(input);
            else
                _externalUserAppService.UpdateUser(input);
            return Json(new ErrorInfo(0, "保存成功"));
        }
        [DontWrapResult]
        public JsonResult GetUser(int Id)
        {
            var user = _externalUserAppService.GetUser(Id);
            return Json(user,JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult SetAvatar(int Id)
        {
            var code = _externalUserAppService.GetCode(Id);
            if (Request.Files != null)
            {
                var avatarFile = Request.Files[0];
                var extension = avatarFile.FileName.Substring(avatarFile.FileName.LastIndexOf("."));
                var filePath = Server.MapPath(@"~/Content/avatars/external/" + code + extension);
                avatarFile.SaveAs(filePath);               
                var fileName = code + extension;              
                _externalUserAppService.SetAvatar(Id, fileName);              
                return Json(new ErrorInfo(0, fileName));
            }
            else
                return Json(new ErrorInfo(-1, "出错了"));
        }
       
        public JsonResult UploadAvatar()
        {         
            if (Request.Files != null)
            {
                var avatarFile = Request.Files[0];
                var extension = avatarFile.FileName.Substring(avatarFile.FileName.LastIndexOf("."));
                var fileName = DateTime.Now.ToFileTime().ToString()+extension;
                var filePath = Server.MapPath(@"~/Content/avatars/external/" +fileName );
                //var virtualPath = @"~/Content/avatars/external/" + fileName;
                avatarFile.SaveAs(filePath);                      
                return Json(new ErrorInfo(0, fileName));
            }
            else
                return Json(new ErrorInfo(-1, "出错了"));
        }

        public string GetAvatar(int Id)
        {
            return _externalUserAppService.GetAvatar(Id);
        }
        public JsonResult RemoveUser(int Id)
        {
            _externalUserAppService.RemoveUser(Id);
            return Json(new ErrorInfo(0, "删除成功"));
        }
    }
}