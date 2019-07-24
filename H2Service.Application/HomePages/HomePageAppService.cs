using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using H2Service.Authorization;
using H2Service.Dto;
using H2Service.Events;
using H2Service.Extensions;
using H2Service.HomePages.Dto;
using H2Service.HomePages.Validate;
using H2Service.MedicalData;
using H2Service.MedicalData.HomePages;
using H2Service.MedicalData.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace H2Service.HomePages
{
 public   class HomePageAppService : H2ServiceAppServiceBase,IHomePageAppService
    {
        private readonly HomePageDomainService _homepageDomainService;      
        private readonly UserDomainService _userDomainService;
        private readonly IRepository<HomePageValidateMessage> _validateMessageRepository;
        private IEventBus _eventBus;
        public HomePageAppService(HomePageDomainService homepageDomainService,       
            UserDomainService userDomainService,
            IRepository<HomePageValidateMessage> validateMessageRepository,
            IEventBus eventBus) {
            _homepageDomainService = homepageDomainService;          
            _eventBus = eventBus;
            _userDomainService = userDomainService;
            _validateMessageRepository = validateMessageRepository;
        }
        /// <summary>
        /// 更新病案首页
        /// </summary>
        /// <param name="admNo">IP就诊记录</param>
        public void UpdateHomePage(string admNo) {           
            _homepageDomainService.UpdateHomepage(admNo);

        }


        /// <summary>
        /// 校验病案首页
        /// </summary>
        /// <param name="pId">就诊ID  Ip开头</param>
        ///  <param name="userNumber">接收消息人员工号</param>
        /// <returns></returns>
        [HttpGet]
        public bool Validate(string pId,string userNumber) {        
            var homePage = _homepageDomainService.GetErpHomePage(pId);
            userNumber = _userDomainService.GetUserById(userNumber);
            var result = true;
            var builder = new StringBuilder();
            //入院时间出院时间必填质控
            if (homePage.RYSJ == null || homePage.RYSJS == null || homePage.CYSJ == null || homePage.CYSJS == null)
            {
                builder.AppendLine("入院时间、出院时间必填");
                result = result && false;
            }
            //年龄质控
            else if(homePage.NL!=null){
                var age =Convert.ToInt32((homePage.RYSJ - homePage.CSRQ).Value.TotalDays / 365);
                if (homePage.NL != age && (homePage.NL + 1) != age && (homePage.NL - 1) != age)
                {
                    builder.AppendLine("年龄误差不能超过1年,计算值为" + age+"填写年龄:"+homePage.NL);
                    result = result && false;
                }
                var Indays = (homePage.CYSJ - homePage.RYSJ).Value.Days == 0 ? 1 : homePage.CYSJ.Value.Subtract(homePage.RYSJ.Value).Days;
                if (Indays != homePage.SJZYTS)
                {
                    builder.AppendLine("住院天数不正确(计算值:"+Indays.ToString()+")");                  
                    result = result && false;
                }
            }
            if (homePage.NL == null)
            {
                builder.AppendLine("年龄不能为空");
                result = result && false;
            }
            if (homePage.RYSJ > homePage.CYSJ)
            {
                builder.AppendLine("入院时间不能晚于出院时间");
                result = result && false;
            }
            if (homePage.CSRQ == null)
            {
                builder.AppendLine("出生日期不能为空");
                result = result && false;
            }
           

            var IDNumber = homePage.SFZH.Trim();
            if (IDNumber != "-")
            {
                if (IDNumber.Length<15)
                {
                    builder.AppendLine("身份证号不能为空如果不知填写-");
                    result = result && false;
                }
                else
                {
                    if (IDNumber.Length == 18)
                    {
                        if (!CheckIDCard18(IDNumber))
                        {
                            builder.AppendLine("身份证号错误");
                            result = result && false;
                        }
                    }
                  else if (IDNumber.Length == 15)
                    {
                        if (!CheckIDCard15(IDNumber))
                        {
                            builder.AppendLine("身份证号错误");
                            result = result && false;
                        }
                    }
                    else
                    {
                        builder.AppendLine("身份证号为15位或者18位");
                        result = result && false;
                    }
                }
            }
            /*******邮编*********/            
            if (string.IsNullOrEmpty(homePage.YB1)) {
                builder.AppendLine("邮编1必填或-");
                result = result && false;
            }
           else if (homePage.YB1.Length>6)
            {
                 builder.AppendLine("邮编1必须为6位数的数字或-");                
            }
            if (string.IsNullOrEmpty(homePage.YB2))
            {
                builder.AppendLine("邮编2必填或-");
                result = result && false;
            }
           else if (homePage.YB2.Length>6)
            {   
                 builder.AppendLine("邮编2必须为6位数的数字或-");
                 result = result && false;                
            }
            if (string.IsNullOrEmpty(homePage.YB3))
            {
                builder.AppendLine("邮编3必填或-");
                result = result && false;
            }
           else if (homePage.YB3.Length > 6)
            {
                builder.AppendLine("邮编3必须为6位数的数字或-");
                result = result && false;
            }


            /*******邮编结束*******/
           
          
            if (homePage.RYTJ == "3")
            {
                if (string.IsNullOrEmpty(homePage.ZZYLJGMC))
                {
                    builder.AppendLine("当入院途径为3时转诊医疗机构不能为空");
                    result = result && false;
                }
            }

            if (homePage.LYFS == "2")
                if (string.IsNullOrEmpty(homePage.YZZY_YLJG) || homePage.YZZY_YLJG == "-")
                {
                    builder.AppendLine("当离院方式为2时上转医疗机构不能为空");
                    result = result && false;
                }
            if (homePage.LYFS == "3")
                if (string.IsNullOrEmpty(homePage.WSY_YLJG) || homePage.WSY_YLJG == "-")
                {
                    builder.AppendLine("当离院方式为3时卫生院名称不能为空");
                    result = result && false;
                }
            if (homePage.YWGM == "2")
                if (string.IsNullOrEmpty(homePage.GMYW)||homePage.GMYW=="-")
                {
                    builder.AppendLine("填写过敏药物名称");
                    result = result && false;
                }
            if (!string.IsNullOrEmpty(homePage.BLZD))
                if (string.IsNullOrEmpty(homePage.BLH)||homePage.BLH=="-")
                {
                    builder.AppendLine("填写病理号");
                    result = result && false;
                }
            if (homePage.ZKRQ < homePage.CYSJ)
            {
                builder.AppendLine("质控日期不能早于出院日期");
                result = result && false;
            }
            if (homePage.XSECSTZ != null)
                if (homePage.XSECSTZ < 100 || homePage.XSECSTZ > 9999)
                {
                    builder.AppendLine("新生儿出生体重填写错误");
                    result = result && false;
                }
            if (homePage.XSERYTZ != null)
                if (homePage.XSERYTZ < 100 || homePage.XSERYTZ > 9999)
                {
                    builder.AppendLine("新生儿入院体重填写错误");
                    result = result && false;
                }
          
            
            if (homePage.RYQ_XS >= 24)
            {
                builder.AppendLine("外伤导致的入院前昏迷小时不能超过24");
                result = result && false;
            }
            if (homePage.RYH_XS >= 24)
            {
                builder.AppendLine("外伤导致的入院后昏迷小时不能超过24");
                result = result && false;
            }
            if (homePage.SFZZYJH == "有")
                if (string.IsNullOrEmpty(homePage.MD))
                {
                    builder.AppendLine("填写31天内再次住院目的");
                    result = result && false;
                }
            if (!string.IsNullOrEmpty(homePage.SSJCZBM1))
            {
                var oper =_homepageDomainService.MapNatOper(homePage.SSJCZBM1);
                if (oper[2] == "介入治疗" || oper[2] == "手术")
                {
                    if (string.IsNullOrEmpty(homePage.SZ1))
                    {
                        builder.AppendLine("主要手术术者不能为空");
                        result = result && false;
                    }
                    if (homePage.SSJCZRQ1 == null)
                    {
                        builder.AppendLine("主要手术操作日期不能为空");
                        result = result && false;
                    }
                    if (string.IsNullOrEmpty(homePage.SSJB1))
                    {
                        builder.AppendLine("主要手术操作级别不能为空");
                        result = result && false;
                    }

                }
                if (oper[2] == "手术")
                {
                    if (string.IsNullOrEmpty(homePage.QKDJ1) || string.IsNullOrEmpty(homePage.MZFS1) || string.IsNullOrEmpty(homePage.MZYS1))
                    {
                        builder.AppendLine("主要手术切口等级、麻醉方式、麻醉医师不能为空");
                        result = result && false;
                    }
                }
            }

            IValidate icdValidate = new HomePageICDValidate(homePage);
            var icdResult = icdValidate.Validate();          
            builder.Append(icdResult.ValidateDescription);
            result = result & icdResult.ValidateResult;

            IValidate depValidate = new HomePageDepValidate(homePage);
            var depResult = depValidate.Validate();
            builder.Append(depResult.ValidateDescription);
            result = result & depResult.ValidateResult;
            _eventBus.Trigger(new HomePageValidateEventData
            {
                Validate = result,
                ValidateMessage = new HomePageValidateMessage
                {
                    AdmNo = homePage.AdmNo,
                    BAH = homePage.BAH,
                    Message = builder.ToString(),
                    SendTime = DateTime.Now,
                    ValidateType=ValidateType.首页填写,
                    DischargeDate=homePage.CYSJ,
                    UserNumber = userNumber,
                    Dep=homePage.CYKBMC
                }
            });
            //if (result)
            //    Logger.Error(homePage.BAH+"校验成功"+"工号:"+userNumber);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool QCValidate(QCValidateInput input) {
            _eventBus.Trigger(new HomePageValidateEventData
            {
                Validate = false,
                ValidateMessage = new HomePageValidateMessage
                {
                    AdmNo = input.AdmNo,
                    BAH = input.BAH,
                    Message = input.Message,
                    SendTime = DateTime.Now,
                    ValidateType = ValidateType.手工质控,
                    DischargeDate = DateTime.Parse(input.DischargeDate),
                    UserNumber = input.UserNumber,
                    SendUser = input.SendUser
                }
            });
           
            return false;
        }
        /// <summary>
        /// 根据用户和校验类型查询校验
        /// </summary>
        /// <param name="type">校验类型</param>
        /// <param name="userNumber">人员工号</param>
        /// <returns></returns>
        public IEnumerable<HomePageValidateMessageDto> GetValidateMessages(string userNumber, ValidateType type = ValidateType.全部) {
            var validateMessages =new List<HomePageValidateMessage>();           
            if (PermissionChecker.IsGranted(PermissionNames.Pages_QC_HomPageAdministrator))
                 validateMessages =_validateMessageRepository.GetAll().OrderByDescending(T=>T.DischargeDate).ToList();
            else
                validateMessages = _validateMessageRepository.GetAll().Where(T => T.UserNumber == userNumber).OrderByDescending(T=>T.DischargeDate).ToList();
           
            return validateMessages.MapTo<IEnumerable<HomePageValidateMessageDto>>();
        }
        
















            private bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准

        }

        private bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();

            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }

            return true;//符合15位身份证标准

        }
        /// <summary>
        /// 获取校验信息列表
        /// </summary>
        /// <param name="userNumber">工号</param>
        /// <returns></returns>
        public List<HomePageValidateMessageDto> GetValidateMessageByUser(string userNumber)
        {
            var result = _validateMessageRepository.GetAll().Where(T => T.UserNumber == userNumber);
            if (result != null)
                return result.MapTo<List<HomePageValidateMessageDto>>();
            else
                return new List<HomePageValidateMessageDto>();
        }
        /// <summary>
        /// 获取校验信息
        /// </summary>
        /// <param name="Id">校验信息Id</param>
        /// <returns></returns>
        public HomePageValidateMessageDto GetValidateMessageById(int Id)
        {
            var msg = _validateMessageRepository.FirstOrDefault(T => T.Id == Id);
            return msg.MapTo<HomePageValidateMessageDto>();
            //if (msg != null)
            //{
            //    var dto= msg.MapTo<HomePageValidateMessageDto>();                
            //    return dto;
            //}
            //else
            //    return new HomePageValidateMessageDto();
        }
    }
}
