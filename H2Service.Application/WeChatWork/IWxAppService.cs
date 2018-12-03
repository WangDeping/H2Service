using Abp.Application.Services;
using H2Service.WeChatWork.Dto;
using H2Service.WeChatWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WeChatWork
{
 public   interface IWxAppService: IApplicationService
    {
        /// <summary>
        /// 创建一个验证url,该url包含重新定向地址
        /// </summary>
        /// <param name="agentid">应用ID</param>
        /// <param name="scope">获取用户保密级别</param>
        /// <returns></returns>
        string GetWxAuthUrl(string scope = "snsapi_base");

        /// <summary>
        /// 获取企业用户,该用户至少包含userid信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        WxUserInfoBaseDto GetWxUserBaseInfoByCode(string code);

        /// <summary>
        /// 根据工号获取微信信息
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        WxUserInfoDetailDto GetWxUserInfoDetail(string UserNumber);
        /// <summary>
        ///下载微信临时文件
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name=" path">保存路径</param>
        /// <returns></returns>
        string DownLoadWxTempFile(string media_id, string path);
       string GetJSApiTicket();
        /// <summary>
        /// 发送企业微信消息
        /// </summary>
        /// <param name="msg">微信消息，可以图文文本等</param>
        /// <returns></returns>
        WxRetMsg SendMsg(WxSendMsg msg);

        List<WxUserInfoDetailDto> GetWxUserInfoListByDeptId(string deptId = "1");

    }
}
