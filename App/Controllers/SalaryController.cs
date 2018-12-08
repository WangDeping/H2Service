using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Controllers;
using H2Service.Extensions;
using H2Service.Salaries;
using H2Service.Salaries.Dto;
using H2Service.WxWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class SalaryController : AppControllerBase
    {
        private ISalaryAppService _salaryAppService;
        private readonly WxTokenManager _wxTokenManager;
        public SalaryController(ISalaryAppService salaryAppService,
            WxTokenManager wxTokenManager) {
            _salaryAppService = salaryAppService;
            _wxTokenManager = wxTokenManager;

        }
        [Authorize]
        public ActionResult PersonalSalary(int Id) {
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            var output = _salaryAppService.GetPersonSalary(new PersonalSalaryInput { PeriodId = Id,
            UserNumber = AbpSession.GetUserNumber() });           
            return View(output);
        }
    }
}