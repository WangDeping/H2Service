using H2Service.HomePages.Dto;
using H2Service.HomePages.Validate;
using H2Service.MedicalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.HomePages
{
    /// <summary>
    /// 首页诊断类验证
    /// </summary>
 public    class HomePageICDValidate:IValidate
    {
        private HomePage _homePage { get; set; }
        private List<string> icds = new List<string>();
        /// <summary>
        /// 构造子
        /// </summary>
        /// <param name="homePage"></param>
        public HomePageICDValidate(HomePage homePage) {
            _homePage = homePage;         
            if (!string.IsNullOrEmpty(homePage.JBDM))
                icds.Add(homePage.JBDM);
            if (!string.IsNullOrEmpty(homePage.JBDM1))
                icds.Add(homePage.JBDM1);
            if (!string.IsNullOrEmpty(homePage.JBDM2))
                icds.Add(homePage.JBDM2);
            if (!string.IsNullOrEmpty(homePage.JBDM3))
                icds.Add(homePage.JBDM3);
            if (!string.IsNullOrEmpty(homePage.JBDM4))
                icds.Add(homePage.JBDM4);
            if (!string.IsNullOrEmpty(homePage.JBDM5))
                icds.Add(homePage.JBDM5);
            if (!string.IsNullOrEmpty(homePage.JBDM6))
                icds.Add(homePage.JBDM6);
            if (!string.IsNullOrEmpty(homePage.JBDM7))
                icds.Add(homePage.JBDM7);
            if (!string.IsNullOrEmpty(homePage.JBDM8))
                icds.Add(homePage.JBDM8);
            if (!string.IsNullOrEmpty(homePage.JBDM9))
                icds.Add(homePage.JBDM9);
            if (!string.IsNullOrEmpty(homePage.JBDM10))
                icds.Add(homePage.JBDM10);
            if (!string.IsNullOrEmpty(homePage.JBDM11))
                icds.Add(homePage.JBDM11);
            if (!string.IsNullOrEmpty(homePage.JBDM12))
                icds.Add(homePage.JBDM12);
            if (!string.IsNullOrEmpty(homePage.JBDM13))
                icds.Add(homePage.JBDM13);
            if (!string.IsNullOrEmpty(homePage.JBDM14))
                icds.Add(homePage.JBDM14);
            if (!string.IsNullOrEmpty(homePage.JBDM15))
                icds.Add(homePage.JBDM15);        


        }
        
        private bool result=true;
        private StringBuilder builder = new StringBuilder();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValidateOutput Validate() {
            if (icds.Count > 0)
            {
                if (icds.FirstOrDefault(T => T.StartsWith("Z37")) != null)
                    if (_homePage.XSECSTZ <= 100)
                    {
                        result = result && false;
                        builder.AppendLine("当诊断中有Z37时新生儿出生体重必填");
                    }

                if (icds.FirstOrDefault(T => T.StartsWith("O80") || T.StartsWith("O81") || T.StartsWith("O82") || T.StartsWith("O83") || T.StartsWith("O84")) != null)
                    if (icds.FirstOrDefault(T => T.StartsWith("Z37")) == null)
                    {
                        result = result && false;
                        builder.AppendLine("当诊断中出现O80-O84,必须有分娩结局Z37编码");
                    }
                if (CannotMainDiagnose.Any(T => icds.First().StartsWith(T)))
                {
                    result = result && false;
                    builder.AppendLine("当前诊断不能作为主诊断");
                }
                if (icds.First().Length < 7)
                {
                    result = result && false;
                    builder.AppendLine("不能用类目和亚目编码，要细目，7位或以上。");
                }
                if (_homePage.H23 != "－" && _homePage.H23 != "-" && !string.IsNullOrEmpty(_homePage.H23) && !"VWXY".Contains(_homePage.H23.ElementAt(0).ToString()))
                {
                    builder.AppendLine("损伤、中毒编码必须以WVXY开头");
                    result = result && false;
                }

            }
            else
            {
                builder.AppendLine("主要诊断不能为空");
                result = result && false;
            }
            if (string.IsNullOrEmpty(_homePage.JBBM)) {

                builder.AppendLine("门诊诊断不能为空");
                result = result && false;
            }
            if (_homePage.JBDM.StartsWith("S") || _homePage.JBDM.StartsWith("T"))
                if (string.IsNullOrEmpty(_homePage.H23)&&_homePage.H23!="-")
                {
                    builder.AppendLine("主要诊断出现S或T时外伤原因必填");
                    result = result && false;
                }
            return new ValidateOutput {  ValidateResult=result, ValidateDescription=builder };
        }


        private List<string> CannotMainDiagnose = new List<string> { "B95","B96","B97", "T31","Z37",
            "Z38", "Z85","Z86","Z87","Z88","Z89","Z90","Z91","Z92" };
    }
}
