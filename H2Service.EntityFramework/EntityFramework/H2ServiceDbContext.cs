﻿using Abp.EntityFramework;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using H2Service.Equipments;
using H2Service.H2Log;
using H2Service.Maintenances;
using H2Service.MedicalWastes;
using H2Service.MeritPays;
using H2Service.QC;
using H2Service.Salarires;
using H2Service.Scheduling;
using H2Service.ServerRooms;
using System.Data.Common;
using System.Data.Entity;

namespace H2Service.EntityFramework
{
    public class H2ServiceDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<SalaryDetail> SalaryDetails { get; set; }
        public virtual IDbSet<SalaryType> SalaryTypes { get; set; }
        public virtual IDbSet<SalaryPeriod> SalaryPeriods { get; set; }
        public virtual IDbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<H2Permission> Permissions { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<DepartmentPermission> DepartmentPermissions { get; set; }
        public virtual IDbSet<ServerRoom> ServerRooms{get;set;}
        public virtual IDbSet<ServerEquipment> ServerEquipments { get; set; }
        public virtual IDbSet<ServerRoomPatrol> ServerRoomPatrols { get; set; }
        public virtual IDbSet<ServerRoomEvent> ServerRoomEvents { get; set; }
        public virtual IDbSet<Department> Departments { get; set; }
        public virtual IDbSet<District> Districts { get; set; }
        public virtual IDbSet<QCAppraisalDetail> DepartmentPunishmentDetails { get; set; }
        public virtual IDbSet<QCAppraisalPeriod> DepartmentPunishmentPeriods { get; set; }
        public virtual IDbSet<ExternalCompany> ExternalCompanys { get; set; }
        public virtual IDbSet<ExternalUser> ExternalUsers { get; set; }
        public virtual IDbSet<MedicalWaste> MedicalWastes { get; set; }
        public virtual IDbSet<MedicalWasteFlow> MedicalWasteFlows { get; set; }
        public virtual IDbSet<MedicalWasteImage> MedicalWasteImages { get; set; }
        public virtual IDbSet<MedicalWasteDelivery> MedicalWasteDeliveries { get; set; }
        public virtual IDbSet<MeritPayPeriod> MeritPayPeriods { get; set; }
        public virtual IDbSet<MeritPayDetail> MeritPayDetail { get; set; }
        public virtual IDbSet<DepartmentRelateModule> DepartmentRelateModules { get; set; }
        public virtual IDbSet<LoginLog> LoginLogs { get; set; }
        public virtual IDbSet<SchedulingGroup> SchedulingGroup { get; set; }
        public virtual IDbSet<SchedulingType> SchedulingTypes{ get; set; }
        public virtual IDbSet<SchedulingRecord> SchedulingRecords { get; set; }
       // public virtual IDbSet<SchedulingDepartment> SchedulingDepartments { get; set; }
        public virtual IDbSet<SchedulingDepartmentUser> SchedulingDepartmentUsers { get; set; }
        public virtual IDbSet<H2ModuleWithAuditing> ModuleWithAuditings { get; set; }
        /// <summary>
        /// 维修工单
        /// </summary>
        public virtual IDbSet<MaintenanceOrder> MaintenanceOrders { get; set; }     
        public virtual IDbSet<MaintenanceTrace> MaintenanceTraces { get; set; }


        public virtual IDbSet<Equipment> Equipments { get; set; }
        public virtual IDbSet<EquipmentKind> EquipmentKinds { get; set; }
        public virtual IDbSet<EquipmentLoanLog> EquipmentLoanLogs { get; set; }
        public virtual IDbSet<EquipmentPatrolLog> EquipmentPatrolLogs { get; set; }
        public virtual IDbSet<EquipmentType> EquipmentTypes { get; set; }
        public virtual IDbSet<EquipmentUsageLog> EquipmentUsageLogs { get; set; }
        public virtual IDbSet<EquipmentProperty> EquipmentProperties { get; set; }
        public virtual IDbSet<EquipmentPatrolDetail> EquipmentPatrolDetails { get; set; }
        public virtual IDbSet<EquipmentModel> EquipmentModels { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public H2ServiceDbContext()
            : base("H2ServiceDb")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in H2ServiceDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of H2ServiceDbContext since ABP automatically handles it.
         */
        public H2ServiceDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public H2ServiceDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public H2ServiceDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
               modelBuilder.Entity<Department>()
                   .HasMany(d=>d.Users)
                       .WithMany(u => u.Departments)
                       .Map(m => {               
                           m.MapLeftKey("DepartmentID");
                           m.MapRightKey("UserID");
                           m.ToTable("DepartmentUsers");
                       })   ;
            modelBuilder.Entity<EquipmentType>()
                   .HasMany(d => d.EquipmentProperties)
                       .WithMany(u => u.EquipmentTypes)
                       .Map(m => {
                           m.MapLeftKey("EquipmentTypeID");
                           m.MapRightKey("EquipmentPropertyID");
                           m.ToTable("EquipmentTypeProterties");
                       });
            modelBuilder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithMany(u => u.Roles)
            .Map(m =>
            {
                m.MapLeftKey("RoleId");
                m.MapRightKey("UserId");
                m.ToTable("RoleUsers");
            });
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Filter(AbpDataFilters.SoftDelete, (ISoftDelete d) => d.IsDeleted, false);
        }
    }
}
