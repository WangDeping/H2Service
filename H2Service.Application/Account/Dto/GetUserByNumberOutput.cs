using Abp.AutoMapper;
using H2Service.Authorization;
using System.ComponentModel.DataAnnotations.Schema;

namespace H2Service.Users.Dto
{
    [AutoMap(typeof(User))]
 public   class GetUserByNumberOutput
    {

        public string UserName { get; set; }

        public int Id { get; set; }

        public string UserNumber { get; set; }

        public string AvatarUrl { get; set; }

        public string TelPhone { get; set; }       
        
    }


}
