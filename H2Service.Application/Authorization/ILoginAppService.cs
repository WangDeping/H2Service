using Abp.Application.Services;
using H2Service.Authorization.Dto;
using System.Security.Claims;

namespace H2Service.Authorization
{
    public interface ILoginAppService:IApplicationService
    {
      void  SignIn(SignInInput user, ClaimsIdentity identity = null);

     void SignOut();

     void   SendValidateCode(string userNumber,string validateCode);
    }
}
