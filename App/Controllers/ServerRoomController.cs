using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using App.Helper;
using Castle.Core.Logging;
using H2Service.Authorization;
using H2Service.ServerRooms;
using H2Service.ServerRooms.Dto;
using H2Service.WxWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [Authorize]
    public class ServerRoomController : AppControllerBase
    {
        private readonly IServerRoomAppService _serverRoomAppService;      
        private readonly WxFileManager _wxFileManager;
        private readonly string wxTempFilePath;
        private readonly WxTokenManager _wxTokenManger;
        public ServerRoomController(IServerRoomAppService serverRoomAppService,    
            WxTokenManager wxTokenManager,
            WxFileManager wxFileManager
            )
        {
            _serverRoomAppService = serverRoomAppService;
            _wxTokenManger = wxTokenManager;
            wxTempFilePath = ConfigurationManager.AppSettings["wxTempFilePath"];
            _wxFileManager = wxFileManager;
        }
        /// <summary>
        /// 机房巡视入口
        /// </summary>
        /// <param name="Id">机房Id</param>
        /// <returns></returns>
        [AbpMvcAuthorize(PermissionNames.Pages_ServerRoom_Partrol)]
        public ActionResult ServerRoomPatrol(int Id=0)
        {
            string ticket = _wxTokenManger.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            var serverRoom = _serverRoomAppService.GetServerRoomById(Id);
            ViewBag.ServerRoomId = Id;
            ViewBag.ServerRoomName = serverRoom.RoomName;
            return View();
        }        

        [DontWrapResult]
        public ActionResult CreatePatrol(CreatePatrolInput serverRoomPatrol)
        {
            if (!string.IsNullOrEmpty(serverRoomPatrol.ImgsPath))
            {
                var tmpArray = serverRoomPatrol.ImgsPath.Split('^');
                var savePath = Server.MapPath(@"~/Content/WxTemp/ServerRoom/");
                var imgsPath = new List<string>();
                foreach (string serverId in tmpArray)
                {
                    //保存http访问路径到数据库
                    Logger.Error("图片保存路径:"+savePath);
                    var url = wxTempFilePath + "ServerRoom/"+_wxFileManager.DownLoadWxTempFile(serverId, savePath);
                    imgsPath.Add(url);
                }
                serverRoomPatrol.ImgsPath = string.Join("^", imgsPath.ToArray());
            }           
            _serverRoomAppService.CreatePartrol(serverRoomPatrol);
            return Json(new ErrorInfo {  Code=0, Message="保存成功"});
        }
    }
}