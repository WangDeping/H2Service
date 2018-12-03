using Abp.Dependency;

namespace H2Service.Hangfire.Framework
{
    public abstract class HangfireJobBase<TJobParams> : ITransientDependency where TJobParams : HangfireParamsInputBase
    {
        /// <summary>
        ///     Called when the job is to be executed.
        /// </summary>    
        /// <param name="aParams">Parameters for the job being executed.</param>
        public abstract void ExecuteJob( TJobParams aParams);
    }
}
