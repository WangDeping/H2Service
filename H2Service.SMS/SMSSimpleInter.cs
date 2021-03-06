﻿using Abp.Dependency;
using Castle.Core.Logging;
using H2Service.SMS.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace H2Service.SMS
{
 public   class SMSSimpleInter: ITransientDependency
    {
        private ILogger _logger;
        public SMSSimpleInter(ILogger logger) {
            _logger = logger;
        }
        /// <summary>
        /// 支持多条短信发送
        /// </summary>
        /// <param name="mobiles"></param>
        /// <param name="content"></param>
        /// <param name="timer"></param>
        /// <param name="customSmsId"></param>
        public void Send(List<string> mobiles,string content,DateTime? timer=null,string customSmsId="") {
            if (mobiles == null)
                return;
            var correctMobiles =mobiles.Where(T=>T.Length==11).ToList();         
            for (var i = 0; i <= correctMobiles.Count / 500; i++)
            {
                var max500Mobiles = mobiles.Skip(i * 500).Take(500).ToList();//超过500手机号分批次发送
                if (max500Mobiles.Count > 0)
                {
                    var baseInput = new SMSSendBaseInput {
                        content = content,
                        mobiles = max500Mobiles,
                        customSmsId = customSmsId,
                        timerTime=timer
                    };
                    this.Send(baseInput);
                }
            }
           
        }      
        private void Send(SMSSendBaseInput input)
        {          
            var url = WebConfigurationManager.AppSettings["smsUrl"]+ @"simpleinter/sendSMS";
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("appId",input.appId);
            parms.Add("timestamp", input.timestamp);
            parms.Add("sign", input.sign);
            parms.Add("timerTime",input.timerTime==null?"":input.timerTime.Value.ToString("yyyyMMddHHmmss"));//定时发送时间            
            parms.Add("customSmsId", input.customSmsId);//自定义customSmsId选填        
            parms.Add("mobiles", string.Join(",",input.mobiles));           
            parms.Add("content","【东营二院】"+input.content);
            var result = SMSHelper.postData(url,parms);
            _logger.Error("发送短信返回："+result);
           
        }
        /// <summary>
        /// 查询余额
        /// </summary>
        public string GetBalance() {
            var url = WebConfigurationManager.AppSettings["smsUrl"] + @"simpleinter/getBalance";
            Dictionary<string, string> parms = new Dictionary<string, string>();
            var entity = new SMSEntityBase();
            parms.Add("appId", entity.appId);
            parms.Add("timestamp", entity.timestamp);
            parms.Add("sign", entity.sign);        
            var result = SMSHelper.postData(url, parms);            
            _logger.Error("余额返回：" + result);
            return result;

        }
        /// <summary>
        /// 获取上行短信
        /// </summary>
        public void GetMo() {
            var url = WebConfigurationManager.AppSettings["smsUrl"] + @"simpleinter/getMo";
            Dictionary<string, string> parms = new Dictionary<string, string>();
            var entity = new SMSEntityBase();
            parms.Add("appId", entity.appId);
            parms.Add("timestamp", entity.timestamp);
            parms.Add("sign", entity.sign);
            parms.Add("number", "500");
            var result = SMSHelper.postData(url, parms);
            _logger.Error("上行：" + result);
        }
    }
}
