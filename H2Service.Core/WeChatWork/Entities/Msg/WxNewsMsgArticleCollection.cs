using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WeChatWork.Entities
{
    public class WxNewsMsgArticleCollection
    {
        public IEnumerable<WxNewsMsgArticle> articles { get; set; }
    }
}
