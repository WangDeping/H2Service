using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using H2Service.Salarires;
using System;
using System.Web;

namespace H2Service.Events.Handler
{
    public class TestEventHandler : IEventHandler<TestEventData>,ITransientDependency
    {

        public TestEventHandler(IRepository<SalaryDetail> detail,ILogger logger)
        {


        }
       
        public void HandleEvent(TestEventData eventData)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("aaa","123"));
           
           
           
            
        }
    }
}
