using Abp;
using H2Service.Authorization;
using H2Service.WeChatWork.Dto;
using H2Service.WeChatWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace H2Service.WeChatWork
{
 public   class WxAppService:H2ServiceAppServiceBase,IWxAppService
    {
        private WxSender _wxSender;
        private WxTokenManager _wxTokenManager;
        private UserManager _userManager;
        private string authAppid;
        private string contactsAppid;
        public WxAppService(WxSender wxSender,WxTokenManager wxTokenManager,
            UserManager userManager) {
            _wxSender = wxSender;
            _wxTokenManager = wxTokenManager;
            contactsAppid = WebConfigurationManager.AppSettings["contactsAppid"];
            authAppid= WebConfigurationManager.AppSettings["authAppid"];
            _userManager = userManager;
        }
       
        /// <summary>
        /// 创建一个验证url,该url包含验证code并且重新定向地址
        /// </summary>
        /// <param name="agentid">应用ID</param>
        /// <param name="scope">获取用户保密级别</param>
        /// <returns></returns>
        public string GetWxAuthUrl(string scope = "snsapi_base") {
            string wxUrl = "";
            string appid = WebConfigurationManager.AppSettings["corpid"];
            string authUrl = WebConfigurationManager.AppSettings["authUrl"];
            wxUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&" +
                    "redirect_uri={1}&response_type=code&scope={2}&" +
                    "agentid={3}&state=STATE&connect_redirect=1#wechat_redirect", appid, authUrl,scope, authAppid);
         
            return wxUrl;
        }
        /// <summary>
        /// 获取企业用户,该用户至少包含userid信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WxUserInfoBaseDto GetWxUserBaseInfoByCode(string code)
        {           
            string token = _wxTokenManager.GetWxToken(authAppid);
            string wxUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?" +
                "access_token={0}&code={1}",token,code);
            var result = Helper.GetURLResultByUTF8(wxUrl);           
            var retAuthUserBase = Helper.JsonDeserialize<WxUserInfoBaseDto>(result);
            if(retAuthUserBase.errcode!=0&&retAuthUserBase.errcode!=40029)
                throw new AbpException("微信错误代码:" +result);
            return retAuthUserBase;

        }
        /// <summary>
        /// 根据工号获取微信用户信息,需要使用通讯录app
        /// </summary>
        /// <param name="UserNumber">工号</param>
        /// <returns></returns>
        public WxUserInfoDetailDto GetWxUserInfoDetail(string UserNumber)
        {
            string token = _wxTokenManager.GetWxToken(contactsAppid);
            string wxUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?" +
                "access_token={0}&userid={1}",token,UserNumber);
            var result = Helper.GetURLResultByUTF8(wxUrl);
            //Logger.Error("获取用户详细返回:"+result);
            var retUserDetail= Helper.JsonDeserialize<WxUserInfoDetailDto>(result);
            if (retUserDetail.errcode != 0) {
                if (retUserDetail.errcode == 60111)
                    Logger.Error("需要删除用户:" + UserNumber);//_userManager.DeleteUser(UserNumber);  
                else
                Logger.Error(string.Format("获取用户{0}失败,返回错误信息{1}", UserNumber, result));
                retUserDetail = null;
            }
            return retUserDetail;
        }
        /// <summary>
        /// 获取部门下所有成员详情，可递归
        /// </summary>
        /// <param name="deptId">部门Id，1代表全院</param>
        /// <returns></returns>
        public List<WxUserInfoDetailDto> GetWxUserInfoListByDeptId(string deptId = "1")
        {
            string token = _wxTokenManager.GetWxToken(contactsAppid);
             string wxUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/list?" +
              "access_token={0}&department_id={1}&fetch_child=1",token,deptId); 
            var result = Helper.GetURLResultByUTF8(wxUrl);           
            var retuserList = Helper.JsonDeserialize<WxUserInfoDetailListDto>(result);           
            if (retuserList.errcode != 0)
                throw new AbpException("微信错误代码:" + result);
            return retuserList.userlist;
        }

        /// <summary>
        /// 根据媒体Id下载保存文件
        /// </summary>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public string DownLoadWxTempFile(string media_id,string path)
        {
            string token = _wxTokenManager.GetWxToken(authAppid);
            string wxUrl =string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",token,media_id);
            return Helper.DownLoadWxTempFile(wxUrl,path);
        }
        public string GetJSApiTicket()
        {
          return  _wxTokenManager.GetWxJSApiTicket();
        }

        public WxRetMsg SendMsg(WxSendMsg msg)
        {
          return  _wxSender.SendMsg(msg);

        }
    }
}
