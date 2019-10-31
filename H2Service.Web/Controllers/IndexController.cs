using H2Service.ServerRooms;
using H2Service.SMS;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace H2Service.Web.Controllers
{
    public class IndexController : H2ServiceControllerBase
    {
        private ServerAppService _serverAppService;
        private ISMSManagerAppService _smsManagerAppService;
        public IndexController(ServerAppService serverAppService,
           ISMSManagerAppService smsManagerAppService)
        {
            _serverAppService = serverAppService;
            _smsManagerAppService = smsManagerAppService;
        }
        // GET: Index
        public ActionResult Index()
        {
         
            return View();
        }
    }
}