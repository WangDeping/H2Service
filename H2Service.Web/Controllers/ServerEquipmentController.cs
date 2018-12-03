using Abp.UI;
using Abp.Web.Models;
using H2Service.Dto;
using H2Service.ServerRooms;
using H2Service.ServerRooms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class ServerEquipmentController : H2ServiceControllerBase
    {
        private IServerEquipmentAppService _serverEquipmentAppService;

        public ServerEquipmentController(IServerEquipmentAppService serverEquipmentAppService)
        {
            _serverEquipmentAppService = serverEquipmentAppService;

        }
        // GET: ServerEquipment
        public ActionResult Index()
        {
            return View();
        }
        [DontWrapResult]
        public JsonResult ServerEquipmentGrid(GetServerEquipmentInput request)
        {
            var result = _serverEquipmentAppService.GetPagedServerEquipment(request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ServerEquipmentTypes()
        {
            string[] allTypes = Enum.GetNames(typeof(SEType));
            return PartialView("_PartialServerEquipmentTypes", allTypes);
        }

        public PartialViewResult ServerOperatingSystems()
        {
            string[] allTypes = Enum.GetNames(typeof(ServerRooms.OperatingSystem));
            return PartialView("_PartialServerOperatingSystems", allTypes);
        }

        public JsonResult AddorUpdateServerEquipment(CreateServerEquipmentInput input)
        {          
                if (input.Id == 0)
                    _serverEquipmentAppService.CreateServerEquipment(input);
                else
                    _serverEquipmentAppService.UpdateServerEquipment(input);

                return Json(new ErrorInfo(0, "保存成功"));           
        }

        [DontWrapResult]
        public JsonResult GetServerInfo(int Id)
        {
            return Json(_serverEquipmentAppService.GetServerEquipmentById(Id));
        }
        /// <summary>
        /// 设置服务器帐号密码(加密)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         public JsonResult SetServerSecurity(ServerSecurityDto input)
        {            
            _serverEquipmentAppService.SetServerAccountAndPassword(input); 
             return Json(new ErrorInfo(0, "保存成功"));           
        }

        /// <summary>
        /// 获取服务器帐号密码(加密)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult GetServerSecurityInfo(int Id)
        {
            return Json(_serverEquipmentAppService.GetServerSecurityById(Id));
        }

        public ActionResult ServerMonitoring()
        {
            List< ServerMonitoringDto > servers = _serverEquipmentAppService.GetMonitoringServers().ToList();
            ViewBag.Servers = servers;
            return View();
        }
    }
}