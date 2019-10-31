using Abp.Application.Services;
using H2Service.SMS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace H2Service.SMS
{
 public  interface ISMSManagerAppService: IApplicationService
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="input"></param>
        void Send(SMSSendInput input);
        /// <summary>
        /// 获取余额
        /// </summary>
        [HttpGet]
        string GetBalance();
    }
}
