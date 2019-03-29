using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models.Requests
{
    public class DeThiRequest
    {
        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public int DonViId { get; set; }
    }
}
