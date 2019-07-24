using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.ICDMaps
{
 public   class ICDMapCaching:DomainService, ISingletonDependency
    {
        private readonly ICacheManager _cacheManager;
        private IRepository<Hos_Nat_ICD10> _hos_Nat_ICD10Repository;
        private IRepository<Hos_Nat_ICD9CM3> _hos_NatOperRepository;
        private IRepository<SD_Nat_ICD10> _sd_NatICD10Repository;
        private IRepository<SD_Nat_ICD9CM3> _sd_NatOperRepository;
        private IRepository<InuseICD10> _inuseICD10Repository;
        private IRepository<InuseICD9CM3> _inuseICD9CM3Repository;

        public ICDMapCaching(ICacheManager cacheManager,
            IRepository<Hos_Nat_ICD10> hos_Nat_ICD10Repository, 
            IRepository<Hos_Nat_ICD9CM3> hos_NatOperRepository,
            IRepository<SD_Nat_ICD10> sd_NatICD10Repository,
            IRepository<SD_Nat_ICD9CM3> sd_NatOperRepository,
            IRepository<InuseICD10> inuseICD10Repository,
            IRepository<InuseICD9CM3> inuseICD9CM3Repository) {
            _cacheManager = cacheManager;
            _hos_Nat_ICD10Repository = hos_Nat_ICD10Repository;
            _hos_NatOperRepository = hos_NatOperRepository;
            _sd_NatICD10Repository = sd_NatICD10Repository;
            _sd_NatOperRepository = sd_NatOperRepository;
            _inuseICD9CM3Repository = inuseICD9CM3Repository;
            _inuseICD10Repository = inuseICD10Repository;
        }
        //private List<Hos_Nat_ICD10> _hosNatICD10Map;
        //private List<Hos_Nat_ICD9CM3> _hostNatOperMap;
        //private List<SD_Nat_ICD10> _sdNatICD10Map;
        //private List<SD_Nat_ICD9CM3> _sdNatOperMap;
        /// <summary>
        /// 获取医院与国家疾病映射表
        /// </summary>
        public List<Hos_Nat_ICD10> HosNatICD10MapCache { get {
                return _cacheManager.GetCache("ICDMap").Get("HosNatICD10Map", () =>
                {
                    return _hos_Nat_ICD10Repository.GetAllList();
                });
            }
        }
        /// <summary>
        /// 获取医院与国家的手术映射表
        /// </summary>
        /// <returns></returns>
        public List<Hos_Nat_ICD9CM3> HosNatOperMapCache {
            get {
                return _cacheManager.GetCache("ICDMap").Get("HosNatOperMap", () =>
                {
                    return _hos_NatOperRepository.GetAllList();
                });
            }
        }
        /// <summary>
        /// 山东版与国家版疾病映射
        /// </summary>
        /// <returns></returns>
        public List<SD_Nat_ICD10> SDNatICD10MapCache {
            get {
                return _cacheManager.GetCache("ICDMap").Get("SDNatICD10Map", () =>
                {
                    return _sd_NatICD10Repository.GetAllList();
                });
            }
        }
        /// <summary>
        /// 山东版与国家版手术映射表
        /// </summary>
        /// <returns></returns>
        public List<SD_Nat_ICD9CM3> SDNatOperMapCache
        {
            get {
                return _cacheManager.GetCache("ICDMap").Get("SDNatOperMap", () =>
                {
                    return _sd_NatOperRepository.GetAllList();
                });
            }
        }

        /// <summary>
        /// 获取在用的ICD10疾病编码库(2019-07-01开始启用)
        /// </summary>
        /// <returns></returns>
        public List<InuseICD10> InuseICD10MapCache {
            get {
                return _cacheManager.GetCache("ICDMap").Get("InuseICD10", () =>
                {
                    return _inuseICD10Repository.GetAllList();
                });
            }
        }
        /// <summary>
        ///  获取在用的ICD9手术编码库(2019-07-01开始启用)
        /// </summary>
        public List<InuseICD9CM3> InuseICD9CM3MapCache
        {
            get
            {
                return _cacheManager.GetCache("ICDMap").Get("InuseICD9CM3", () =>
                {
                    return _inuseICD9CM3Repository.GetAllList();
                });
            }
        }
    }
}
