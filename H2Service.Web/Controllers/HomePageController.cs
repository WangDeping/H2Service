using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Extensions;
using H2Service.HomePages;
using H2Service.Web.Helpers;
using H2Service.Web.Models.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomePageController : H2ServiceControllerBase
    {

        private readonly IHomePageAppService _homePageAppservice;

        public HomePageController(IHomePageAppService homePageAppservice) {
            _homePageAppservice = homePageAppservice;

        }

        // GET: HomePage
        public ActionResult Index()
        {            
            return View();
        }
        /// <summary>
        /// 首页质控表格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult MessageGrid(HomePageValidateMessageGridModel request) {

            var userNumber = AbpSession.GetUserNumber();
            var result = _homePageAppservice.GetValidateMessages(userNumber);
            var items = result.Skip(request.SkipCount).Take(request.MaxResultCount).ToList();
            return Json(new { total = result.Count(), rows = items }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExportGrid() {
            var userNumber = AbpSession.GetUserNumber();
            var result = _homePageAppservice.GetValidateMessages(userNumber).Where(T => !string.IsNullOrEmpty(T.DischargeDate)).Select(T => new HomePageValidateMessageExcelModel
            {
                Dep = T.Dep,
                DischargeDate = T.DischargeDate,
                Message = T.Message,
                SendTime = T.SendTime,
                UserNumber=T.UserNumber
            }).ToList();
            var shortName = @"/tmpFiles/" + DateTime.Now.ToFileTime().ToString() + ".xlsx";
            var fileName = Server.MapPath(@"~/" + shortName);
            var headerArrary = new string[] { "科室", "出院日期", "主管医师工号","问题", "发送时间", };
            ExcelHelper.ExportEasy(result, fileName, headerArrary);
            return Json(shortName);
        }
    }
}