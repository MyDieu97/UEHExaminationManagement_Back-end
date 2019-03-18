using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class ViPhamGV
    {
        public int Id { get; set; }

        public string NoiDung { get; set; }

        public string HinhThucXuLy { get; set; }

        public int GVCoiThiId { get; set; }

        [ForeignKey("GVCoiThiId")]
        public virtual GiangVien_CoiThi GiangVien_CoiThi { get; set; }
    }
}
