using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.Account.Dto;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using H2Service.Dto;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account
{
    public class DepartmentAppService : H2ServiceAppServiceBase, IDepartmentAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<DepartmentRelateModule> _relationRepository;
        private readonly IDepartmentDomainService _departmentDomainService;
        private readonly IRepository<DepartmentPermission> _departmentPermissionRepository;
        private List<DepartmentDto> _ancestorsList;
        private List<DepartmentDto> _descendantsList;
        public DepartmentAppService(IRepository<Department> departmentRepository,
           IDepartmentDomainService departmentDomainService,
           IRepository<DepartmentPermission> departmentPermissionRepository,
           IRepository<DepartmentRelateModule> relationRepository)
        {

            _departmentRepository = departmentRepository;
            _departmentDomainService = departmentDomainService;
            _departmentPermissionRepository = departmentPermissionRepository;
            _relationRepository = relationRepository;
            _ancestorsList = new List<DepartmentDto>();
            _descendantsList = new List<DepartmentDto>();
        }

        public IEnumerable<DepartmentTreeOutput> GetDepartmentTree(string parent)
        {
            if (parent == "#")
            {
                return GetDepartmentTree();
            }
            else
            {
                var parentDepartment = _departmentRepository.Get(int.Parse(parent));
                if (parentDepartment.FatherId == 0)
                    return GetDepartmentTree();
                else
                {
                    var descendantsList = DepartmentWithDescendants(int.Parse(parent));
                    var items = new List<DepartmentTreeOutput>();
                    foreach (var tree in descendantsList)
                        if (tree.Id.ToString() != parent)
                            items.Add(new DepartmentTreeOutput { id = tree.Id, text = tree.DepartmentName, parent = tree.FatherId.ToString() });
                    return items;
                }
            }
        }
        private IEnumerable<DepartmentTreeOutput> GetDepartmentTree()
        {
            var items = _departmentRepository.GetAll().OrderByDescending(T => T.Order).Select(T => new DepartmentTreeOutput
            {
                id = T.Id,
                text = T.DepartmentName,
                parent = T.FatherId.ToString() == "0" ? "#" : T.FatherId.ToString()
            });
            return items;

        }
        public DepartmentDto GetRootDepartment()
        {
            return _departmentRepository.FirstOrDefault(T => T.FatherId == 0).MapTo<DepartmentDto>();
        }

        [AbpAuthorize(PermissionNames.Pages_System_Department)]
        public void CreateDepartment(DepartmentDto dto)
        {

            _departmentRepository.Insert(ObjectMapper.Map<Department>(dto));

        }

        [AbpAuthorize(PermissionNames.Pages_System_Department)]
        public void UpdateDepartment(DepartmentDto dto)
        {
            _departmentRepository.Update(ObjectMapper.Map<Department>(dto));

        }
        public DepartmentDto GetById(int Id)
        {
            var dep = _departmentRepository.FirstOrDefault(Id);
            if (dep == null)
                throw new UserFriendlyException("该部门不存在");
            return ObjectMapper.Map<DepartmentDto>(dep);
        }

        [AbpAuthorize(PermissionNames.Pages_System_Department)]
        public void ChangeFather(DepartmentDto dto)
        {
            var department = _departmentRepository.Get(dto.Id);
            if (department != null)
            {
                var updateDepartment = ObjectMapper.Map<Department>(department);
                updateDepartment.FatherId = dto.FatherId;
                _departmentRepository.Update(ObjectMapper.Map<Department>(updateDepartment));

            }

        }


        /// <summary>
        /// 获取用户所属的部门
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public List<DepartmentUserDto> GetUserInDepartments(long Id)
        {

            var departments = _departmentRepository.GetAll().Where(T => T.Users.Where(U => U.Id == Id).Count() > 0).Select(D => new DepartmentUserDto { DepartmentId = D.Id, UserId = Id, DepartmentName = D.DepartmentName });

            return departments.ToList();
        }

        /// <summary>
        /// 获取部门下的用户
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public List<UserDto> GetUserByDepartmentId(int Id)
        {
            var users = _departmentRepository.FirstOrDefault(T=>T.Id==Id).Users.ToList();
            return users.MapTo<List<UserDto>>();
        }

        /// <summary>
        /// 分配人员到科室
        /// </summary>
        /// <param name="dto"></param>
        public void AssginDepartment(DepartmentUserDto dto)
        {

            var departments = _departmentRepository.Get(dto.DepartmentId).Users.FirstOrDefault(U => U.Id == dto.UserId);
            if (departments != null)
                throw new UserFriendlyException("该用户已经在此部门中");
            else
                _departmentDomainService.AssginDepartment(dto.UserId, dto.DepartmentId);
        }

        public void RemoveDepartmentUser(DepartmentUserDto dto)
        {

            _departmentDomainService.DeleteDepartmentUser(dto.UserId, dto.DepartmentId);

        }

        [AbpAuthorize(PermissionNames.Pages_System_Department)]
        public void RemoveDepartment(int Id)
        {

            _departmentDomainService.DeleteDepartmentUser(Id);

        }


        public IEnumerable<DepartmentDto> DepartmentWithAncestors(int Id)
        {
            var department = this.GetById(Id);
            _ancestorsList.Add(department);
            var ancestorsDepartment = _departmentRepository.FirstOrDefault(department.FatherId);
            if (ancestorsDepartment != null)
                DepartmentWithAncestors(ancestorsDepartment.Id);
            return _ancestorsList;
        }
        /// <summary>
        /// 获取该部门及所有子孙部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<DepartmentDto> DepartmentWithDescendants(int Id)
        {
            var departmentEntity = _departmentRepository.FirstOrDefault(Id);
            if (departmentEntity != null)
            {
                var department = departmentEntity.MapTo<DepartmentDto>();
                var childrenDepartments = _departmentRepository.GetAll().Where(D => D.FatherId == Id);
                _descendantsList.Add(department);
                if (childrenDepartments.Count() == 0)
                    department.IsLeaf = true;
                foreach (var child in childrenDepartments)
                {
                    DepartmentWithDescendants(child.Id);
                }
            }
            return _descendantsList;

        }

        /// <summary>
        /// 获取所有允许存在用户的部门
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentDto> GetDepartmentAllowedUser()
        {
            var result = _departmentRepository.GetAll().Where(T => T.IsAllowedHasUser == true).OrderBy(T=>T.DepartmentName).ToList();
            return result.MapTo<IEnumerable<DepartmentDto>>();
        }


        [AbpAuthorize(PermissionNames.Pages_System_Permission)]
        public void GrantPermission(GrantPermissionInput input)
        {
            _departmentPermissionRepository.Delete(T => T.DepartmentId == input.DepartmentId);
            var departments = this.DepartmentWithDescendants((int)input.DepartmentId);
            foreach (var child in departments)
            {
                var dep = _departmentRepository.Get(child.Id);
                foreach (var permission in input.Permissions)
                {
                    if (!dep.Permissions.Any(P => P.PermissionName == permission))
                        dep.Permissions.Add(new DepartmentPermission { PermissionName = permission, DepartmentId = dep.Id });
                }
            }

        }

        public void CreateDepartmentModuleRelation(DepartmentModulesRelationDto dto)
        {
            var department = GetById(dto.DepartmentId);
            if (_relationRepository.FirstOrDefault(T => T.DepartmentId == dto.DepartmentId && T.Module == dto.Module) != null)
                throw new UserFriendlyException(department.DepartmentName + "已经关联到此系统模块");
            if (dto.Module == H2Module.医疗废物 && department.District == null)
                throw new UserFriendlyException(department.DepartmentName + "未设置院区位置");
            var ancestors = DepartmentWithAncestors(department.Id).ToList();//部门的祖先部门
            var descendants = DepartmentWithDescendants(department.Id).ToList();//部门的子孙部门
            var relatedDepartments = GetRelatedDepartments((int)dto.Module);//已经关联到该模块的部门

            var ancestors_redundant = ancestors.FindAll(T => relatedDepartments.Select(S => S.DepartmentId).Contains(T.Id));//??new List<DepartmentDto>();
            var descendants_redundant = descendants.FindAll(T => relatedDepartments.Select(S => S.DepartmentId).Contains(T.Id));//??new List<DepartmentDto>();

            if ((descendants_redundant.Count) > 0)
                throw new UserFriendlyException(department.DepartmentName + "与" + descendants_redundant.First().DepartmentName + "存在冲突");
            if (ancestors_redundant.Count > 0)
                throw new UserFriendlyException(department.DepartmentName + "与" + ancestors_redundant.First().DepartmentName + "存在冲突");
            _relationRepository.Insert(ObjectMapper.Map<DepartmentRelateModule>(dto));
        }

        public List<DepartmentModulesRelationDto> GetRelatedDepartments(int module)
        {
            var list = _relationRepository.GetAll().Where(T => (int)T.Module == module).OrderBy(T => T.Department.DepartmentName).ToList();
            return list.MapTo<List<DepartmentModulesRelationDto>>();
        }

        public void RemoveRelation(int Id)
        {
            _relationRepository.Delete(Id);
        }
    }
}
