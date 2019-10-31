using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Dto;
using H2Service.HomePages.Dto;
using H2Service.MedicalData.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace H2Service.HomePages
{
 public  interface IHomePageAppService:IApplicationService
    {
        /// <summary>
        /// 更新首页
        /// </summary>
        /// <param name="admNo">就诊记录</param>
        void UpdateHomePage(string admNo);
        /// <summary>
        /// 首页保存验证
        /// </summary>
        /// <param name="pId">就诊ID</param>
        /// <param name="userNumber"></param>
        /// <returns></returns>
        [HttpGet]
        bool Validate(string pId,string userNumber);

        IEnumerable<HomePageValidateMessageDto> GetValidateMessages(string userNumber, ValidateType type = ValidateType.全部);

        HomePageValidateMessageDto GetValidateMessageById(int Id);
        /// <summary>
        /// 住院总质控病历
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool QCValidate(QCValidateInput input);
        /// <summary>
        /// 完善病历后通知住院总
        /// </summary>
        /// <param name="Id">通知Id</param>
        void CorrectThenNotify(int Id);
        /// <summary>
        /// 住院总审核通过问题病历
        /// </summary>
        /// <param name="Id">消息Id</param>
        void PassValidate(int Id);
        /// <summary>
        /// 住院总审核回退
        /// </summary>
        /// <param name="Id"></param>
        void NoPassValidate(int Id);
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="Id">Id</param>
        void DeleteMessage(int Id);
        /// <summary>
        /// 病案归档上架
        /// </summary>
        /// <param name="bah"></param>
        void FileHomePage(string bah);
    }
}
