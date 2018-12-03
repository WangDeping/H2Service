using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Helper
{
 public   class UserNumberConversionHelper
    {
        public static Dictionary<string, string> UserNumberConversionDictionary()
        {
            Dictionary<string, string> userNumberDicionary = new Dictionary<string, string>();
            userNumberDicionary.Add("1066", "lansecheng");
            userNumberDicionary.Add("0001", "01");
            userNumberDicionary.Add("0002", "02");
            userNumberDicionary.Add("0003", "03");
            userNumberDicionary.Add("288", "0288");
            userNumberDicionary.Add("1961", "1904");            
            return userNumberDicionary;
        }
    }
}
