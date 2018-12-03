using H2Service.ServerRooms;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class IndexController : H2ServiceControllerBase
    {
        private ServerAppService _serverAppService;
        public IndexController(ServerAppService serverAppService)
        {
            _serverAppService = serverAppService;
        }
        // GET: Index
        public ActionResult Index()
        {
            var result = _serverAppService.GetServerInfo();
            ViewBag.Server = result;
            return View();
        }
    }
}