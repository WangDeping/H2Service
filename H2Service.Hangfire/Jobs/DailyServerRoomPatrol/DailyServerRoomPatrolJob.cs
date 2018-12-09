using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using H2Service.Hangfire.Framework;
using H2Service.Hangfire.Jobs.DailyServerRoomPatrol.Dto;
using H2Service.Log4;
using H2Service.ServerRooms;
using H2Service.ServerRooms.Dto;
using H2Service.WxWork;
using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace H2Service.Hangfire.Jobs.DailyServerRoomPatrol
{
    public class DailyServerRoomPatrolJob : HangfireJobBase<DailyServerRoomPatrolJobArgs>
    {
    
       
        private IRepository<ServerRoom> _serverRoomRepository;
        private readonly IRepository<ServerRoomPatrol> _serverRoomPartrolRepository;
        private IRepository<Department> _departmentRepository;
        private IRepository<DepartmentRelateModule> _departmentRelateModuleRepository;
        private readonly WxSender _wxSender;
        private ILogAppService _logAppservice;
        public DailyServerRoomPatrolJob(    
            IRepository<ServerRoom> serverRoomRepository,
            IRepository<ServerRoomPatrol> serverRoomPartrolRepository,
             IRepository<Department> departmentRepository,
             IRepository<DepartmentRelateModule> departmentRelateModuleRepository,
             WxSender wxSender,
             ILogAppService logAppservice)
        {
           
            _serverRoomRepository = serverRoomRepository;
            _serverRoomPartrolRepository = serverRoomPartrolRepository;
            _departmentRepository = departmentRepository;
            _departmentRelateModuleRepository =departmentRelateModuleRepository;
            _logAppservice = logAppservice;
            _wxSender = wxSender;
        }
        [UnitOfWork]
        public override void ExecuteJob(DailyServerRoomPatrolJobArgs aParams)
        {
            if (WebConfigurationManager.AppSettings["DailyServerRoomPatrolJob"] == "1")
            {
                var deps = _departmentRelateModuleRepository.GetAll().Where(T => T.Module == H2Module.机房科室);
                foreach (var dep in deps)
                {
                    var rooms = _serverRoomRepository.GetAll().Where(T => T.DepartmentId == dep.DepartmentId);
                    if (rooms == null || rooms.Count() == 0)
                    {
                        _logAppservice.LogError(dep.Department.DepartmentName + "科室下无机房");
                        return;
                    }
                    var unPatrolRooms = new List<ServerRoomDto>();
                    var input = new GetPatrolsInput
                    {
                        DateStart = DateTime.Now.Date,
                        DateEnd = DateTime.Now.Date.AddDays(1)
                    };
                    foreach (var room in rooms)
                    {
                        input.RoomId = room.Id;
                        var unPatrolRoom = _serverRoomPartrolRepository.GetAll().WhereIf(input.RoomId != 0, t => t.ServerRoomId == input.RoomId)
                        .WhereIf(input.DateStart.HasValue, t => t.CreationTime >= input.DateStart)
                        .WhereIf(input.DateEnd.HasValue, t => t.CreationTime <= input.DateEnd).ToList().MapTo<List<ServerRoomPatrolDto>>();

                        if (unPatrolRoom.Count == 0)//今天没有查询到巡视数据
                            unPatrolRooms.Add(room.MapTo<ServerRoomDto>());
                    }
                    if (unPatrolRooms.Count == 0)//全都巡视了
                        return;

                    string unPatrolRoomNames = "";//没有去巡视机房的名字
                    foreach (var room in unPatrolRooms)
                    {
                        unPatrolRoomNames += room.RoomName+"|";
                    }
                    var sentContent = string.Format("截止到{0},{1}没有去巡视,请尽快前去巡视并做好记录", DateTime.Now, unPatrolRoomNames);
                  
                    var msg = new WxSendTextMsg(sentContent, dep.Department.Users.Select(T => T.UserNumber).JoinAsString("|"));
                    _wxSender.SendMsg(msg);
                }
            }
        }
    }
}
