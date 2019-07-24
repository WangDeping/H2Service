using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class WxSendNewsMsg : WxSendMsg
    {
        private WxSendNewsMsg()
        {
          
        }
        /// <summary>
        /// 可序列JSON
        /// </summary>
        [JsonProperty]
        private WxNewsMsgArticleCollection news { get; set; }       
        public WxSendNewsMsg(string description,string picurl,string title,string url,string touser,string agentid="0") {
            news = new WxNewsMsgArticleCollection();
            this.msgtype = "news";
            this.touser = touser;
            this.agentid = agentid;
            news.articles = new List<WxNewsMsgArticle>() {
                new WxNewsMsgArticle{
                    description =description,
                    picurl=picurl,
                    title=title,
                    url=url
                }
            };
          
        }
    }
}
