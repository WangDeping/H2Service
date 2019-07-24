using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace H2Service.EntityFramework.Repositories
{
    public abstract class H2ServiceRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<H2ServiceDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected H2ServiceRepositoryBase(IDbContextProvider<H2ServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class H2ServiceRepositoryBase<TEntity> : H2ServiceRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected H2ServiceRepositoryBase(IDbContextProvider<H2ServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }

//    public abstract class HomePageRepository<TEntity, TPrimaryKey> : EfRepositoryBase<MedicalDataContext, TEntity, TPrimaryKey>
//where TEntity : class, IEntity<TPrimaryKey>
//    {
//        protected HomePageRepository(IDbContextProvider<MedicalDataContext> dbContextProvider) : base(dbContextProvider)
//        {
//        }
//    }
}
