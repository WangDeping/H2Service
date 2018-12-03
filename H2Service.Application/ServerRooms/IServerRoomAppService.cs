using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Dto;
using H2Service.ServerRooms.Dto;
using System.Collections.Generic;

namespace H2Service.ServerRooms
{
    public   interface IServerRoomAppService: IApplicationService
    {
        /// <summary>
        /// 添加机房巡视记录
        /// </summary>
        /// <param name="serverRoomPatrol"></param>
        void CreatePartrol(CreatePatrolInput serverRoomPatrol);
        
        /// <summary>
        /// 分页查询机房巡视记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResultDto<ServerRoomPatrolDto> GetPagedServerRoomPatrols(int? roomId,PagedInputDto input);



        /// <summary>
        /// 添加机房
        /// </summary>
        /// <param name="serverRoom"></param>
        void CreateServerRoom(ServerRoomDto serverRoom);
        /// <summary>
        /// 修改机房信息
        /// </summary>
        /// <param name="serverRoom"></param>
        void UpdateServerRoom(ServerRoomDto serverRoom);
        /// <summary>
        /// 获取机房信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ServerRoomDto GetServerRoomById(int Id);
        /// <summary>
        /// 分页查询机房列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResultDto<ServerRoomDto> GetPagedServerRooms(PagedInputDto input);
        /// <summary>
        /// 多条件查询机房巡视
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IList<ServerRoomPatrolDto> GetServerRoomPatrols(GetPatrolsInput input);
        /// <summary>
        /// 机房事件登记
        /// </summary>
        /// <param name="input"></param>
        void CreateEvent(CreateServerRoomEventInput input);
        /// <summary>
        /// 机房事件查询
        /// </summary>
        /// <param name="roomId">机房id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResultDto<ServerRoomEventDto> GetPagedServerRoomEvents(int? roomId, PagedInputDto input);

        /// <summary>
        /// 获取所有机房 列表
        /// </summary>
        /// <returns></returns>
        List<ServerRoomDto> GetAllRooms();
    }
}
