using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.HomePage
{
    public class HomePageValidateMessageGridModel: PagedInputDto
    {
        public string UserNumber { get; set; }
    }
}