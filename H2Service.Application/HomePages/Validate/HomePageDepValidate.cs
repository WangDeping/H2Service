using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2Service.HomePages.Dto;
using H2Service.MedicalData;

namespace H2Service.HomePages.Validate
{
    public class HomePageDepValidate : IValidate
    {
        private HomePage _homePage { get; set; }
        private bool result = true;
        private StringBuilder builder = new StringBuilder();
        /// <summary>
        /// 构造子
        /// </summary>
        /// <param name="homePage"></param>
        public HomePageDepValidate(HomePage homePage) {

            _homePage = homePage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValidateOutput Validate()
        {
            if (!string.IsNullOrEmpty(_homePage.ZKKB) && (_homePage.ZKKB.Contains("无") || _homePage.ZKKB.Contains("病区")))
            {
                builder.AppendLine("转科科别要么空要么填写-或科室");
                result = result && false;
            }
            if (!string.IsNullOrEmpty(_homePage.ZKKB1) && (_homePage.ZKKB1.Contains("无") || _homePage.ZKKB.Contains("病区")))
            {
                builder.AppendLine("转科科别要么空要么填写-或科室");
                result = result && false;
            }
            if (!string.IsNullOrEmpty(_homePage.ZKKB2) && (_homePage.ZKKB2.Contains("无") || _homePage.ZKKB.Contains("病区")))
            {
                builder.AppendLine("转科科别要么空要么填写-或科室");
                result = result && false;
            }
            if (!string.IsNullOrEmpty(_homePage.RYKB) && _homePage.RYKB.Contains("病区"))
            {
                builder.AppendLine("入院科室不能填写病区");
                result = result && false;
            }
            if (!string.IsNullOrEmpty(_homePage.CYKB) && _homePage.CYKB.Contains("病区"))
            {
                builder.AppendLine("出院科室不能填写病区");
                result = result && false;
            }
            return new ValidateOutput { ValidateResult = result, ValidateDescription = builder };
        }
    }
}
