using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Runtime.Session;
using Abp.UI;
using H2Service.Authorization;
using H2Service.Dto;
using H2Service.Extensions;
using H2Service.ServerRooms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    [AbpAuthorize(PermissionNames.Pages_ServerRoom)]
    public   class ServerRoomAppService : H2ServiceAppServiceBase,IServerRoomAppService
    {
        private readonly IRepository<ServerRoom> _serverRoomRepository;
        private readonly IRepository<ServerRoomPatrol> _serverRoomPartrolRepository;
        private readonly IRepository<ServerEquipment> _serverEquipmentRepository;
        private readonly IRepository<ServerRoomEvent> _serverRoomEventRepository;
        private readonly IEventBus _eventBus;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public ServerRoomAppService(IRepository<ServerRoom> serverRoomRepository,
            IRepository<ServerRoomPatrol> serverRoomPartrolRepository,
           IRepository<ServerEquipment> serverEquipmentRepository,
           IRepository<ServerRoomEvent> serverRoomEventRepository,
           IEventBus eventBus,
           IUnitOfWorkManager unitOfWorkManager) {
            _serverRoomRepository = serverRoomRepository;
            _serverRoomPartrolRepository = serverRoomPartrolRepository;
            _serverEquipmentRepository = serverEquipmentRepository;
            _serverRoomEventRepository = serverRoomEventRepository;
            _eventBus = eventBus;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 巡视机房
        /// </summary>
        /// <param name="serverRoomPatrol"></param>       
        public void CreatePartrol(CreatePatrolInput serverRoomPatrol)
        {
             _serverRoomPartrolRepository.Insert(ObjectMapper.Map<ServerRoomPatrol>(serverRoomPatrol));           
        }
        /// <summary>
        /// 机房巡视表
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<ServerRoomPatrolDto> GetPagedServerRoomPatrols(int? roomId,PagedInputDto input)
        {
            var depId = AbpSession.GetDepartmentId();
            if (string.IsNullOrEmpty(depId))
                throw new UserFriendlyException("您没有分配科室");
            var int_depId = int.Parse(depId);
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = roomId == null ? _serverRoomPartrolRepository.GetAll().Where(T => T.ServerRoom.DepartmentId == int_depId).OrderByDescending(T=>T.CreationTime) : _serverRoomPartrolRepository.GetAll().Where(e => e.ServerRoomId == roomId).OrderByDescending(T => T.CreationTime);
                var partrolsCount = query.Count();
                var partrolsList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                return new PagedResultWithSumDto<ServerRoomPatrolDto> { Items = partrolsList.MapTo<List<ServerRoomPatrolDto>>(), TotalCount = partrolsCount };
            }
         
         
        }
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IList<ServerRoomPatrolDto> GetServerRoomPatrols(GetPatrolsInput input)
        {
            var query = _serverRoomPartrolRepository.GetAll().WhereIf(input.RoomId != 0, t => t.ServerRoomId == input.RoomId)
                .WhereIf(input.DateStart.HasValue, t => t.CreationTime >= input.DateStart)
                .WhereIf(input.DateEnd.HasValue, t => t.CreationTime <= input.DateEnd);
            var patrolsList = query.ToList();
            return patrolsList.MapTo<List<ServerRoomPatrolDto>>();
        }
        /// <summary>
        /// 机房事件登记
        /// </summary>
        /// <param name="input"></param>
        public void CreateEvent(CreateServerRoomEventInput input)
        {
            _serverRoomEventRepository.Insert(ObjectMapper.Map<ServerRoomEvent>(input));

        }
        /// <summary>
        /// 机房事件列表
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<ServerRoomEventDto> GetPagedServerRoomEvents(int? roomId,PagedInputDto input)
        {
            var query =roomId==null? _serverRoomEventRepository.GetAll():_serverRoomEventRepository.GetAll().Where(e=>e.ServerRoomId==roomId);
            var eventsCount = query.Count();
            var eventsList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<ServerRoomEventDto> { Items = eventsList.MapTo<List<ServerRoomEventDto>>(), TotalCount = eventsCount };
        }
        /// <summary>
        /// 增加机房
        /// </summary>
        /// <param name="serverRoom"></param>
        [AbpAuthorize(PermissionNames.Pages_ServerRoom_Info)]
        public void CreateServerRoom(ServerRoomDto serverRoom) {
            if (serverRoom != null)
                _serverRoomRepository.Insert(serverRoom.MapTo<ServerRoom>());
            else
                throw new UserFriendlyException("机房为空");
        }
        public void UpdateServerRoom(ServerRoomDto serverRoom)
        {
            if (serverRoom != null)
                _serverRoomRepository.Update(serverRoom.MapTo<ServerRoom>());
            else
                throw new UserFriendlyException("机房为空");
        }
        public ServerRoomDto GetServerRoomById(int Id)
        {
           var serverRoom= _serverRoomRepository.FirstOrDefault(s=>s.Id==Id);
            return serverRoom.MapTo<ServerRoomDto>();
        }
        public List<ServerRoomDto> GetAllRooms()
        {
            var rooms = _serverRoomRepository.GetAllList();
            return rooms.MapTo<List<ServerRoomDto>>();

        }
        [AbpAuthorize(PermissionNames.Pages_ServerRoom_Info)]
        public PagedResultDto<ServerRoomDto> GetPagedServerRooms(PagedInputDto input)
        {
            var depId = AbpSession.GetDepartmentId();
            if (string.IsNullOrEmpty(depId))
                throw new UserFriendlyException("您没有分配科室");
            var int_depId = int.Parse(depId);
            var query = _serverRoomRepository.GetAll().Where(T => T.DepartmentId == int_depId);
            var serverRoomsCount = query.Count();
            var serverRoomsList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<ServerRoomDto> { Items = serverRoomsList.MapTo<List<ServerRoomDto>>(), TotalCount = serverRoomsCount };
        }
    }
}
