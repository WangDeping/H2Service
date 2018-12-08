using Abp.Dependency;
using Abp.Domain.Services;
using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork
{
 public   interface IWxWorkService: ITransientDependency
    {
       
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<WxRetMsg> SendMsgAsync(WxSendMsg msg);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        WxRetMsg SendMsg(WxSendMsg msg);

        
    }
}
