using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class DeThi
    {
        public int Id { get; set; }

        public string MaLopHP { get; set; }

        public string TenHocPhan { get; set; }

        public DateTime NgayGioThi { get; set; }

        public string DonVi { get; set; }
    }
}
