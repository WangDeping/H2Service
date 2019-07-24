using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Waste
{
    public class DeliveryCollectionRequestModel
    {
        public int DistrictId { get; set; }
        public string[] ImagesUrl { get; set; }
    }
}