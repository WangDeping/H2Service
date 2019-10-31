using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.HomePageValidate
{
    public enum ValidateTypeModel
    {
        全部 = 0,
        首页填写 = 1,//首页填写校验
        手工质控 = 2,//手工质控校验
        出院日期一致性 = 3,//出院日期一致性校验
        住院总质控 = 4,
        临床改正反馈 = 5
    }
}