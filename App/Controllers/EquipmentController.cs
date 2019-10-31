using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using App.Models.Equipment;
using H2Service.Account;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using H2Service.Dto;
using H2Service.Equipments;
using H2Service.Equipments.Dto;
using H2Service.Extensions;
using H2Service.Helpers;
using H2Service.Users;
using H2Service.WxWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace App.Controllers
{
    [AbpMvcAuthorize]
    public class EquipmentController : AppControllerBase
    {
        private readonly WxTokenManager _wxTokenManager;
        private readonly IUserAppService _userAppService;
        private readonly IDepartmentAppService _deptAppService;
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly IEquipmentPatrolAppService _equipmentPatrolAppService;
        private readonly IEquipmentUsageAppService _usageAppService;
        private readonly IEquipmentLoanAppService _loanAppService;
        public EquipmentController(WxTokenManager wxTokenManager,
            IUserAppService userAppService, IDepartmentAppService deptAppService, 
            IEquipmentAppService equipmentAppService,IEquipmentPatrolAppService equipmentPatrolAppService
            , IEquipmentUsageAppService usageAppService, IEquipmentLoanAppService loanAppService
            ) {
            _wxTokenManager = wxTokenManager;
            _userAppService = userAppService;
            _deptAppService = deptAppService;
            _equipmentAppService = equipmentAppService;
            _equipmentPatrolAppService = equipmentPatrolAppService;
            _usageAppService = usageAppService;
            _loanAppService = loanAppService;
        }
        // GET: Equipment
        public ActionResult Index()
        {
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            var userId=AbpSession.GetUserId();
            var depIds = _deptAppService.GetUserInDepartments(userId).Select(T=>T.DepartmentId).ToList();
            var searchInput = new GetEquipmentInput { DepIds = depIds, MaxResultCount = 10000, PageNumber = 0};
            var equipments = _equipmentAppService.GetPagedEquipment(searchInput);
            var deps = _deptAppService.GetRelatedDepartments((int)H2Module.设备管理);
            return View(new EquipmentIndexModel { EquipmentGrids=equipments.Items, RelatedDepts=deps });
        }

        public ActionResult PatrolView(string code) {
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);           
            var equipment = _equipmentAppService.GetEquipment(new GetEquipmentInput {  Code=code});
            ViewBag.Date = DateTime.Now.ToString();
            ViewBag.UserName = AbpSession.GetUserName();
            ViewBag.PatrolType = PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Equipment_PatrolII).Result ? PatrolTypeEnum.II级巡视 : PatrolTypeEnum.I级巡视;
            return View(equipment);
        }
        [HttpPost]
        public JsonResult Patrol(CreatePatrolModel request) {           
            var patrolInput = new CreatePatrolInput
            {
                CreatorUserId = AbpSession.GetUserId(),
                Type =PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Equipment_PatrolII).Result?PatrolTypeEnum.II级巡视:PatrolTypeEnum.I级巡视,
                EquipmentId = request.EquipmentId,
                Summary = request.Summary,
                MainCheck=request.MainCheck
            };           
            foreach (var detail in request.Details) {               
                patrolInput.PatrolDetails.Add(new EquipmentPatrolDetailDto
                {
                    PropertyId = detail.Key,
                    Result = detail.Value
                });
            }
            try
            {
                _equipmentPatrolAppService.CreatePatrol(patrolInput);
                return Json(new { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                throw ex;
             //return   Json(new { Code = -1, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
       
        public  PartialViewResult PatrolLogsView(GetPatrolLogsModel request) {
            var today = DateTime.Now.Date;
            var isAmd = PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Equipment_PatrolII).Result;
            var currendDepId =int.Parse(AbpSession.GetDepartmentId());
            var input = new GetPatrolLogsInput
            {
                Begin = today.AddDays(-6),
                End = today.AddDays(1),
                Code = request.Code,
                PageNumber = request.PageNumber,
                MaxResultCount = request.MaxResultCount,                
                Type = isAmd ? PatrolTypeEnum.II级巡视 : PatrolTypeEnum.I级巡视
            };
            if (isAmd)
                input.DepartmentId = null;
            else
                input.DepartmentId = currendDepId;
            var result = _equipmentPatrolAppService.GetPatrolLogs(input);
            return PartialView("_GetPatrolLogs", new PagedPatrolLogsResultModel { total = result.TotalCount, rows = result.Items });

        }

        public PartialViewResult SearchEquipment(GetEquipmentInput request) {
            var equipments = _equipmentAppService.GetPagedEquipment(request);
            return PartialView("_GetGrids", equipments.Items);
        }

        public ActionResult UsageView(string code) {
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            var equipment = _equipmentAppService.GetEquipment(new GetEquipmentInput { Code = code });
            return View(equipment);
        }

        public PartialViewResult UsageLogsView(GetUsagelLogsInput request) {
            var input = new GetUsagelLogsInput { EquipmentId = request.EquipmentId, MaxResultCount = request.MaxResultCount, PageNumber = request.PageNumber };
            var result = _usageAppService.GetPatrolLogs(input);            
            return PartialView("_GetUsageLogs",new PagedUsageLogsResultModel { total = result.TotalCount, rows = result.Items });
        }
       [DontWrapResult]
        public JsonResult CreateUsage(CreateUsageModel request) {
            var input = new CreateUsageInput
            {
                BeginTime = request.BeginTime,
                EquipmentId = request.EquipmentId,
                PatientAdmNo = request.PatientAdmNo,
                BeginUserId = AbpSession.GetUserId()
            };
            try
            {
                _usageAppService.CreateUsage(input);
                return Json(new { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(new { Code = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [DontWrapResult]
        public JsonResult EndUsage(int Id) {
            var input = new EndUsageInput() { EndTime = DateTime.Now, EndUserId = AbpSession.GetUserId(), Id = Id };
            _usageAppService.EndUsage(input);
            return Json(new { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }

        public ViewResult PreviewLoan(PreviewLoanModel request) {
            var user = _userAppService.GetUserByNumber(request.UserNumber);            
            if (user == null)
                return View("Empty", new ErrorInfo { Message = "没有找到该用户:"+request.UserNumber });
            var equipment = _equipmentAppService.GetEquipment(new GetEquipmentInput { Code = request.Code });
          
            if (equipment.IsLoaning_Id==0|| equipment.IsLoaning_Id==null)  //设备未借出返回借出界面
            {     
                var defaultDep = _userAppService.GetDefaultDepartment(user.Id);
                if (defaultDep == null)
                    return View("Empty", new ErrorInfo { Message = "借用人无归属科室" });
                if(equipment.DepartmentId.ToString()!=AbpSession.GetDepartmentId())
                    return View("Empty", new ErrorInfo { Message = "非设备归属科室人员操作" });
                var display = new PreviewLoanModel
                {
                    Id=0,
                    BorrowUserId = user.Id,
                    EquipmentId = equipment.Id,
                    Code = request.Code,
                    BorrowUserName =user.UserName,
                    EquipmentName = equipment.EquipmentName,
                    LoanTime=DateTime.Now,
                    LoanUserId = AbpSession.GetUserId(),
                    LoanUserName = AbpSession.GetUserName(),
                    BorrowDepId = defaultDep.Id,
                    BorrowDepName = defaultDep.DepartmentName,
                    LoanDepId = equipment.DepartmentId,
                    LoanDepName = equipment.DepartmentName
                };
                display.QrCode = (new
                {
                    BorrowUserId = display.BorrowUserId,
                    EquipmentId = display.EquipmentId,
                    BorrowDepId = display.BorrowDepId,
                    LoanUserId = display.LoanUserId,
                    LoanDepId = display.LoanDepId
                }).ModelToUriParam(WebConfigurationManager.AppSettings["appBaseUrl"] + "Equipment/ConfirmLoanView/");
                return View(display);
            }
            else//,设备借出状态转到归还界面
            {
                var loanLog = _loanAppService.GetLoanLog(equipment.IsLoaning_Id.Value);
                var display = new PreviewLoanModel
                {
                    Id=loanLog.Id,
                    Code=equipment.Code,
                    EquipmentName = equipment.EquipmentName,
                    BorrowDepName = loanLog.BorrowDeptName,
                    BorrowUserName = loanLog.BorrowUserName,
                    LoanDepName = loanLog.LoanDepartmentName,
                    LoanUserName = loanLog.LoanUserName,
                    LoanTime = loanLog.LoanTime,
                    ReturnUserName = user.UserName,
                    ReturnTime = DateTime.Now,
                    SignUserName = AbpSession.GetUserName()
                };
                display.QrCode = (new
                {
                    Id= equipment.IsLoaning_Id.Value,
                    ReturnUserId = user.Id,
                    SignUserId =AbpSession.GetUserId()
                }).ModelToUriParam(WebConfigurationManager.AppSettings["appBaseUrl"] + "Equipment/ConfirmReturnView/");
                return View(display);
            }
        }

        public ActionResult ConfirmQrCode(string param) {  
            var stream = QrCodeHelper.CommonQrCode(param);
            return File(stream.ToArray(), "image/jpeg");
        }
        public ActionResult ConfirmLoanView(PreviewLoanModel request) {
            var info = new ErrorInfo();
            if (request.BorrowUserId.Value != AbpSession.GetUserId())
            {
                info.Code = -99;
                info.Message = "非借用人扫码";
            }
            else
            {
                var input = new LoanEquipmentInput
                {
                    BorrowDeptId = request.BorrowDepId,
                    BorrowUserId = request.BorrowUserId.Value,
                    EquipmentId = request.EquipmentId,
                    LoanDeptId = request.LoanDepId,
                    LoanUserId = request.LoanUserId.Value,
                    LoanTime = DateTime.Now
                };
                try
                {
                    _loanAppService.LoanEqupment(input);
                    info.Code = 0; info.Message = "操作成功";
                }
                catch (UserFriendlyException ex)
                {
                    info.Code = ex.Code; info.Message = ex.Message;info.Details = ex.Details;
                }
            }
            return View("ConfirmView",info);
        }
        public ActionResult ConfirmReturnView(PreviewLoanModel request) {
            var info = new ErrorInfo();
            if (request.ReturnUserId.Value != AbpSession.GetUserId())
            {
                info.Code = -99;
                info.Message = "非归还人扫码";
            }
            else {
                var input = new DoneLoanLogInput
                {
                    Id = request.Id,
                    ReturnUserId=request.ReturnUserId.Value,
                    SignUserId =request.SignUserId.Value
                };
                try
                {
                    _loanAppService.DoneLoanLog(input);
                    info.Code = 0; info.Message = "操作成功";
                }
                catch (UserFriendlyException ex)
                {
                    info.Code = ex.Code; info.Message = ex.Message; info.Details = ex.Details;
                }

            }
            return View("ConfirmView", info);
        }

        public PartialViewResult LoanLogsView()
        {
            var input = new GetPagedLoanlLogsInput
            {
                DepartmentId = int.Parse(AbpSession.GetDepartmentId()),
                MaxResultCount = 20,
                PageNumber = 0
            };
            var result = _loanAppService.GetPagedLoanLogs(input);
            return PartialView("_GetLoanLogs", new PagedLoanLogsResultModel { total = result.TotalCount, rows = result.Items });

        }
    }
}