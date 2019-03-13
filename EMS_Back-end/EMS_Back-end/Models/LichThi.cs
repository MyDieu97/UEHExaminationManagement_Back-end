using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class LichThi
    {
        public int Id { get; set; }

        public string HeDaoTao { get; set; }

        public string LopSV { get; set; }

        public string Khoa { get; set; }

        public string ThoiKB { get; set; }

        public DateTime NgayGioBDThi { get; set; }

        public string Thu { get; set; }

        public string CSThi { get; set; }

        public string PhongThi { get; set; }

        public int SoSV { get; set; }

        public string HinhThuThi { get; set; }
    }
}
