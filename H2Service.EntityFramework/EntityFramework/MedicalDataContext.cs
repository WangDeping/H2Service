using Abp.EntityFramework;
using H2Service.MedicalData;
using System.Data.Entity;
using H2Service.MedicalData.ICDMaps;
using H2Service.MedicalData.HomePages;
using H2Service.MedicalData.OPMedicalDiagnose;

namespace H2Service.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public   class MedicalDataContext: AbpDbContext
    {   
        /// <summary>
        /// 首页
        /// </summary>
        public virtual IDbSet<HomePage> HomePages { get; set; }
        /// <summary>
        /// 医院版与国家版的疾病映射
        /// </summary>
        public virtual IDbSet<Hos_Nat_ICD10> Hos_Nat_ICD10Maps { get; set; }
        /// <summary>
        /// 医院版与国家版的手术映射
        /// </summary>
        public virtual IDbSet<Hos_Nat_ICD9CM3> Hos_Nat_ICD9CM3Maps { get; set; }
        /// <summary>
        /// 山东版与国家版的疾病映射
        /// </summary>
        public virtual IDbSet<SD_Nat_ICD10> SD_Nat_ICD10Maps { get; set; }
        /// <summary>
        /// 山东版与国家版的手术编码映射
        /// </summary>
        public virtual IDbSet<SD_Nat_ICD9CM3> SD_Nat_ICD9CM3 { get; set; }


        public virtual IDbSet<HomePageValidateMessage> HomePageValidateMessages { get; set; }

        /// <summary>
        /// 在用疾病编码
        /// </summary>
        public virtual IDbSet<InuseICD10> InUserICD10 { get; set; }
        /// <summary>
        /// 在用手术编码
        /// </summary>
        public virtual IDbSet<InuseICD9CM3> InuseICD9CM3 { get; set; }

        public virtual IDbSet<OPMedicalDiagnose> OPMedicalDiagnose { get; set; }

        public MedicalDataContext(): base("MySql")
        {
            Database.SetInitializer<MedicalDataContext>(null);
           
        }

        //public MedicalDataContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{

        //}

        ////This constructor is used in tests
        //public MedicalDataContext(DbConnection existingConnection)
        // : base(existingConnection, false)
        //{

        //}

        //public MedicalDataContext(DbConnection existingConnection, bool contextOwnsConnection)
        // : base(existingConnection, contextOwnsConnection)
        //{

        //}
    }
}
