using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.Authorization;
using H2Service.Dto;
using H2Service.Helpers;
using H2Service.ServerRooms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    [AbpAuthorize(PermissionNames.Pages_ServerRoom)]
    public class ServerEquipmentAppService : H2ServiceAppServiceBase, IServerEquipmentAppService
    {
        private readonly IRepository<ServerEquipment> _serverEquipmentRepository;
        private const string decryptKey = "dje3mdi1";

        public ServerEquipmentAppService(IRepository<ServerEquipment> serverEquipmentRepository)
        {
            _serverEquipmentRepository = serverEquipmentRepository;
        }
        /// <summary>
        /// 添加服务器/设备
        /// </summary>
        public void CreateServerEquipment(CreateServerEquipmentInput input)
        {
            if (input != null)
            {
                if (!string.IsNullOrEmpty(input.IP) && _serverEquipmentRepository.FirstOrDefault(s => s.SEName == input.SEName || s.IP == input.IP) != null)
                    throw new UserFriendlyException(-1, "IP或者设备名称有重复");
                else
                    _serverEquipmentRepository.Insert(ObjectMapper.Map<ServerEquipment>(input));
            }

            else
                throw new UserFriendlyException(-1, "服务器设备信息为空");
        }
        /// <summary>
        /// 更新服务器/设备
        /// </summary>
        /// <param name="input"></param>
        public void UpdateServerEquipment(UpdateServerEquipmentInput input)
        {                      
            var serverEquipment = _serverEquipmentRepository.Get(input.Id);
            ObjectMapper.Map(input,serverEquipment);   //实现部分字段更新         
            _serverEquipmentRepository.Update(serverEquipment);
        }

        public PagedResultDto<ServerEquipmentDto> GetPagedServerEquipment(GetServerEquipmentInput input)
        {
            var query = _serverEquipmentRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.IP), t => t.IP == input.IP)
                .WhereIf(input.SEType != "-1", t => Enum.GetName(typeof(SEType), t.SEType.Value) == input.SEType);
            var serverEquipmentsCount = query.Count();
            var serverEquipmentsList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<ServerEquipmentDto> { Items = serverEquipmentsList.MapTo<List<ServerEquipmentDto>>(), TotalCount = serverEquipmentsCount };

        }
        
        /// <summary>
        /// 设置登录帐号密码
        /// </summary>
        /// <param name="dto"></param>
        public void SetServerAccountAndPassword(ServerSecurityDto dto)
        {
            if (dto.Id > 0)
            {
                var server =_serverEquipmentRepository.Get(dto.Id);
                if (server == null)
                    throw new UserFriendlyException(-1, "服务器对象为空");
                if (server.SEType.Value != SEType.服务器 && server.SEType.Value != SEType.虚拟机)
                    throw new UserFriendlyException(-1, "仅允许服务器、虚拟机进行本操作");               
                var serverEquipment = ObjectMapper.Map<ServerEquipment>(server);
                serverEquipment.Account = dto.Account;
                serverEquipment.Password= SecurityHelper.EncryptDES(dto.Password, decryptKey);
                serverEquipment.IsMonitored = dto.IsMonitored;
                _serverEquipmentRepository.Update(serverEquipment);
            }
            else
                throw new UserFriendlyException(-1, "未找到该服务器信息");
        }

        /// <summary>
        /// 根据Id获取服务器/设备信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ServerEquipmentDto GetServerEquipmentById(int Id)
        {
            var serverEquipment = _serverEquipmentRepository.Get(Id);
            return serverEquipment.MapTo<ServerEquipmentDto>();
        }

        public ServerSecurityDto GetServerSecurityById(int Id)
        {
            var server = _serverEquipmentRepository.Get(Id);
            return server.MapTo<ServerSecurityDto>();
        }

        /// <summary>
        /// 获取所有监控服务器及监控状态
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ServerMonitoringDto> GetMonitoringServers()
        {
            var list= _serverEquipmentRepository.GetAllList(t=>t.IsMonitored==true).MapTo<List<ServerEquipmentDto>>();
            foreach(var serverEquipment in list)
            {
                ServerMonitoringDto dto = new ServerMonitoringDto {
                    ServerEquipment = serverEquipment,
                    ServerMonitoring=this.GetMonitoringInfo(serverEquipment)
                };
                yield return dto;
            }

        }
        /// <summary>
        /// 根据IP 帐号 密码获取服务器信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ServerMonitoringInfo GetMonitoringInfo(ServerEquipmentDto dto)
        {
            var securityDto = GetServerSecurityById(dto.Id);            
            var _serverWMICoreManager = new ServerWMICoreManger(dto.IP,securityDto.Account , SecurityHelper.DecryptDES(securityDto.Password, decryptKey));
            return _serverWMICoreManager.server;
           
        }
    }
}
