using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Dto;
using H2Service.External.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.External
{
 public   interface IExternalUserAppService: IApplicationService
    {
        ExternalUserDto GetUser(int Id);

        void UpdateUser(ExternalUserDto input);

        void CreateUser(ExternalUserDto input);

        PagedResultDto<ExternalUserDto> GetPagedExternalUsers(GetExternalUsersInput input);

        string GetCode(int Id);

        void SetAvatar(int Id, string avatarUrl);

        string GetAvatar(int Id);

        void RemoveUser(int Id);

        GetExternalUserByCodeOutput GetUserByCode(string code);


    }
}
