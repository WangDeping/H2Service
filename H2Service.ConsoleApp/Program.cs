using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var test = new WxTest();
            test.SendMsgToPresident();

            Console.ReadLine();
        }
    }
}
