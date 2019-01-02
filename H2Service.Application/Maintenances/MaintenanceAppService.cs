using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using H2Service.Maintenances.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H2Service.Maintenances
{
    public   class MaintenanceAppService:H2ServiceAppServiceBase,IMaintenanceAppService
    {
        private readonly IRepository<MaintenanceOrder> _orderRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderRepository"></param>
        public MaintenanceAppService(IRepository<MaintenanceOrder> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        /// <summary>
        /// 派单的异步方法
        /// </summary>
        /// <param name="input"></param>
        public async Task<int> DispatchMaintenanceOrderAsync(DispatchMaintenanceOrderInput input) {
            var order = input.MapTo<MaintenanceOrder>();
            order.Status = MaintenanceOrderStatus.派单分配;
            return await _orderRepository.InsertAndGetIdAsync(order);           
        }
        /// <summary>
        /// 派单的同步方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int DispatchMaintenanceOrder(DispatchMaintenanceOrderInput input)
        {
            var order = input.MapTo<MaintenanceOrder>();
            order.Status = MaintenanceOrderStatus.派单分配;
            return _orderRepository.InsertAndGetId(order);
        }
        /// <summary>
        /// 维修工单分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<GetPagedMaintenanceOrdersOutput> GetPagedMaintenanceOrders(GetPagedMaintenanceOrdersInput input)
        {
            var query = _orderRepository.GetAll();
            var orders = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<GetPagedMaintenanceOrdersOutput> { Items = orders.MapTo<List<GetPagedMaintenanceOrdersOutput>>(), TotalCount = query.Count() };
        }
        /// <summary>
        /// 扫码完成默认全好评
        /// </summary>
        /// <param name="input"></param>
        public void CompleteOrderByQrCode(CompleteOrderBaseInput input)
        {
            var order = _orderRepository.Get(input.Id);
            if (order.Status >= MaintenanceOrderStatus.工单完成)
                throw new Exception("工单已完成");
            order.Status = MaintenanceOrderStatus.扫码完成;
            order.RecorderId = input.RecorderId;
            order.CompletionTime = DateTime.Now;
            order.ArrivalSpeed = 100;
            order.Quality = 100;
            order.Service = 100;
            order.RepairEfficiency = 100;            
        }
        /// <summary>
        /// 客服回访完成并评分
        /// </summary>
        /// <param name="input"></param>
        public void CompleteOrderByRecorder(CompleteOrderInput input)
        {
            var order = _orderRepository.Get(input.Id);
            if (order.Status >= MaintenanceOrderStatus.工单完成)
                throw new Exception("工单已完成");
            order.RecorderId = input.RecorderId;
            order.CompletionTime = DateTime.Now;
            order.Remarks = input.Remarks;
            order.RepairEfficiency = input.RepairEfficiency;
            order.Service = input.Service;
            order.Quality = input.Quality;
            order.ArrivalSpeed = input.ArrivalSpeed;
            order.Status = MaintenanceOrderStatus.工单完成;            
        }

        public void RemoveOrder(int Id)
        {
            var order = _orderRepository.Get(Id);
            if (order != null)
                _orderRepository.Delete(order);
        }
    }
}
