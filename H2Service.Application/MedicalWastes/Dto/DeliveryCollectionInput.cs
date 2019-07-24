using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
    /// <summary>
    /// 暂存处出库参数
    /// </summary>
 public   class DeliveryCollectionInput
    {
        public int DistrictId { get; set; }
        public List<string> ImagesUrl { get; set; }
    }
}
