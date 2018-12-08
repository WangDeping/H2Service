using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
   internal class WxNewsMsgArticleCollection
    {
        internal WxNewsMsgArticleCollection()
        {
            articles = new List<WxNewsMsgArticle>();

        }
        [JsonProperty]
        internal IEnumerable<WxNewsMsgArticle> articles { get; set; }
    }
}
