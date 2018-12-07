using Abp.UI;
using Abp.Web.Models;
using H2Service.Account;
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
using System.Web.Configuration;
using H2Service.Users;
using H2Service.Web.Models.Users;
using H2Service.Users.Dto;
using H2Service.WeChatWork;

namespace H2Service.Web.Controllers
{
    public class ExternalCompanyController : H2ServiceControllerBase
    {
        private readonly IExternalCompanyAppService _externalCompanyAppService;
        private readonly IExternalUserAppService _externalUserAppService;
        private readonly IDepartmentAppService _departmentAppService;
        private readonly IUserAppService _userAppService;
        private readonly WxFileManager _wxFileManager;
        public ExternalCompanyController(IExternalCompanyAppService externalCompanyAppService,
            IExternalUserAppService externalUserAppService,
            IDepartmentAppService departmentAppService,
            IUserAppService userAppService,
            WxFileManager wxFileManager
            )
        {
             _externalCompanyAppService = externalCompanyAppService;
            _externalUserAppService = externalUserAppService;
            _departmentAppService = departmentAppService;
            _userAppService = userAppService;
            _wxFileManager = wxFileManager;

        }

        public ActionResult Index()
        {
            return View();
        }
        [DontWrapResult]
        public JsonResult CompaniesGrid(PagedInputDto request)
        {
            var rootCompanyId =int.Parse(WebConfigurationManager.AppSettings["externalLocalDepartmentRootId"]);
            var result = _departmentAppService.DepartmentWithDescendants(rootCompanyId).Where(T => T.Id != rootCompanyId);
           // var result = _externalCompanyAppService.GetPagedExternalCompanies(request);
            return Json(new { total = result.Count(), rows = result }, JsonRequestBehavior.AllowGet);
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
        public JsonResult UsersGrid(GetExternalUsersInput request)
        {          
            var result = _userAppService.GetUsersDepartmentWithDescendants(request.ExternalCompanyId);
            return Json(new { total = result.Count, rows = result}, JsonRequestBehavior.AllowGet);
        }
    
        public JsonResult AddOrUpdateUser(ExternalUserModel input)
        {           
            var remoteDepartmetnId = int.Parse(WebConfigurationManager.AppSettings["externalRemoteDepartmentRootId"]);
            input.RemoteDepartmentId = remoteDepartmetnId;
            if (input.Id == 0)
            {
                var userDto = new UserDto {
                     AvatarUrl=input.AvatarUrl,
                     Gender=(Authorization.Gender)input.Gender,
                     TelPhone=input.TelPhone,
                     UserName=input.UserName,                     
                     UserNumber=input.UserNumber                     
                };
                try
                {
                    _externalUserAppService.CreateUser(userDto, input.DepartmentId, remoteDepartmetnId);
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException(ex.Message);
                }
            }
            //else
            //    _externalUserAppService.UpdateUser(input);
            return Json(new ErrorInfo(0, "保存成功"));
        }
      
        public JsonResult SetAvatar(int Id)
        {          
            if (Request.Files != null)
            {
                var avatarFile = Request.Files[0];
                var buf = new byte[avatarFile.InputStream.Length];
                avatarFile.InputStream.Read(buf, 0, (int)avatarFile.InputStream.Length);
                var result= _wxFileManager.UploadTempFile(avatarFile.FileName,buf,"image");
                if(result.errcode==0)
                 return Json(new ErrorInfo(0,result.media_id));
                else
                    return Json(new ErrorInfo(-1, result.errmsg));
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
       
    }
}