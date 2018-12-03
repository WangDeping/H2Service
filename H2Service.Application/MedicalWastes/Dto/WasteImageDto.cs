using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace H2Service.MedicalWastes.Dto
{
    [AutoMap(typeof(MedicalWasteImage))]
    public   class WasteImageDto
    {
        //private string _imgPath;
        public int Id { get; set; }

        public MedicalWasteStatus Status { get; set; }

        public int FlowId { get; set; }

        public string ImgPath {get;set;
            //get {
            //    if (HttpContext.Current.Request.Url.ToString().Contains(WebConfigurationManager.AppSettings["intranet"]))
            //        return (_imgPath.Replace(WebConfigurationManager.AppSettings["extranet"], WebConfigurationManager.AppSettings["intranet"]));
            //    else
            //        return _imgPath;

            //}
            //set { _imgPath = value; }
        }
    }
}
