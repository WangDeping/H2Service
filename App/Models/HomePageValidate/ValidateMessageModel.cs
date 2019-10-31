using H2Service.HomePages.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.HomePageValidate
{
    public class ValidateMessageModel
    {
        public HomePageValidateMessageDto Msg { get; set; }
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdministrator { get; set; }
        /// <summary>
        /// 是否是住院总
        /// </summary>
        public bool IsQCDoctor { get; set; }
        /// <summary>
        /// 当前用户是发送人
        /// </summary>
        public bool IsSender { get; set; }
        /// <summary>
        /// 是否是当事人
        /// </summary>
        public bool IsSelf { get; set; }

        public string SenderUserName { get; set; }

        public string UserName { get; set; }

    }
}