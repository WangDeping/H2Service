using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Authorization;
using H2Service.Dto;
using H2Service.Extensions;
using H2Service.Helpers;
using H2Service.ServerRooms;
using H2Service.ServerRooms.Dto;
using System.Configuration;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ServerRoomController : H2ServiceControllerBase
    {
        private readonly IServerRoomAppService _serverRoomAppService;
        public ServerRoomController(IServerRoomAppService serverRoomAppService)
        {
            var depId = AbpSession.GetDepartmentId();
            if (string.IsNullOrEmpty(depId))
                throw new UserFriendlyException("您没有分配科室");
            _serverRoomAppService = serverRoomAppService;

        }

       
        public ActionResult Index()
        {
            return View();
        }
        [AbpMvcAuthorize(PermissionNames.Pages_ServerRoom)]
        public ActionResult ServerRoomIndex()
        {          
            return View();
        }
        [DontWrapResult]     
        [AbpMvcAuthorize(PermissionNames.Pages_ServerRoom_Info)]
        public JsonResult ServerRoomGrid(PagedInputDto request)
        {
            var result = _serverRoomAppService.GetPagedServerRooms(request);
            return  Json(new {  total=result.TotalCount, rows=result.Items  },JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult ServerRoomList()
        {
            var allRooms =_serverRoomAppService.GetAllRooms();
            return PartialView("_PartialServerRoomList", allRooms);
           
        }
       
        public JsonResult AddorUpdateServerRoom(ServerRoomDto input)
        {
            if (input.Id == 0)
                _serverRoomAppService.CreateServerRoom(input);
            else
                _serverRoomAppService.UpdateServerRoom(input);
            return Json(new ErrorInfo(0,"保存成功"));
        }
        [DontWrapResult]
        public JsonResult GetServerRoom(int Id)
        {
            return Json(_serverRoomAppService.GetServerRoomById(Id));
        }
        [DontWrapResult]
        public ActionResult ServerRoomQrcode(int Id)
        {
            string url =string.Format(ConfigurationManager.AppSettings["serverRoomQrcode"],Id);           
            var stream = QrCodeHelper.CommonQrCode(url);
            return File(stream.ToArray(), "image/jpeg");
          
        }

        public ActionResult PatrolIndex()
        {

            return View();
        }
        [DontWrapResult]
        public JsonResult PatrolList(int roomId,PagedInputDto request)
        {
            int? arg = null;
            if (roomId != 0)
                arg = roomId;
            var result = _serverRoomAppService.GetPagedServerRoomPatrols(arg,request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EventIndex()
        {
            return View();
        }
    }
}