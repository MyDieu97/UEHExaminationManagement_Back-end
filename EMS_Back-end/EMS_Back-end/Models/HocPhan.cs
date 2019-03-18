using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class HocPhan
    {
        public int Id { get; set; }

        public string MaHP { get; set; }

        public int SoTinChi { get; set; }

        public string HeDaoTao { get; set; }

        public int DonViRaDeId { get; set; }

        public int DonViQuanLyId { get; set; }

        [ForeignKey("DonViRaDeId")]
        public virtual DonVi DonViRaDe { get; set; }

        [ForeignKey("DonViQuanLyId")]
        public virtual DonVi DonViQuanLy { get; set; }
    }
}
