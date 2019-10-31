using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2Service.HomePages.Dto;
using H2Service.MedicalData;

namespace H2Service.HomePages.Validate
{
    public class HomePageDateValidate : IValidate
    {
        private HomePage _homePage { get; set; }
        private bool result = true;
        private StringBuilder builder = new StringBuilder();
        public HomePageDateValidate(HomePage homePage) {
            _homePage = homePage;
        }
        public ValidateOutput Validate()
        {            

            if (_homePage.RYSJ == null || _homePage.RYSJS == null || _homePage.CYSJ == null || _homePage.CYSJS == null)
            {
                builder.AppendLine("入院时间、出院时间必填");
                result = result && false;
            }
            //年龄质控
            else if (_homePage.NL != null)
            {
                var age = Convert.ToInt32((_homePage.RYSJ - _homePage.CSRQ).Value.TotalDays / 365);
                if (_homePage.NL != age && (_homePage.NL + 1) != age && (_homePage.NL - 1) != age)
                {
                    builder.AppendLine("年龄误差不能超过1年,计算值为" + age + "填写年龄:" + _homePage.NL);
                    result = result && false;
                }
                var Indays = (_homePage.CYSJ - _homePage.RYSJ).Value.Days == 0 ? 1 : _homePage.CYSJ.Value.Subtract(_homePage.RYSJ.Value).Days;
                if (Indays != _homePage.SJZYTS)
                {
                    builder.AppendLine("住院天数不正确(计算值:" + Indays.ToString() + ")");
                    result = result && false;
                }
            }
            if (_homePage.NL == null)
            {
                builder.AppendLine("年龄不能为空");
                result = result && false;
            }
            if (_homePage.RYSJ > _homePage.CYSJ)
            {
                builder.AppendLine("入院时间不能晚于出院时间");
                result = result && false;
            }
            if (_homePage.CSRQ == null)
            {
                builder.AppendLine("出生日期不能为空");
                result = result && false;
            }
            if (_homePage.ZKRQ == null) {
                builder.AppendLine("质控日期不能为空");
                result = result && false;
            }
            if (_homePage.ZKRQ < _homePage.CYSJ) {
                builder.AppendLine("质控日期不能早于出院日期");
                result = result && false;
            }

            if (_homePage.RYQ_XS >= 24)
            {
                builder.AppendLine("外伤导致的入院前昏迷小时不能超过24");
                result = result && false;
            }
            if (_homePage.RYH_XS >= 24)
            {
                builder.AppendLine("外伤导致的入院后昏迷小时不能超过24");
                result = result && false;
            }
            return new ValidateOutput { ValidateDescription = builder, ValidateResult = result };
        }
    }
}
