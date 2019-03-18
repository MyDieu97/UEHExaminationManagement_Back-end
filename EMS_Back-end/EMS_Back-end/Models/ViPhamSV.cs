using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class ViPhamSV
    {
        public int Id { get; set; }

        public string MSSV { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public string MucDoVP { get; set; }

        public string HinhThucXuLy { get; set; }

        public int LopHPId { get; set; }

        [ForeignKey("LopHPId")]
        public virtual LopHocPhan LopHP { get; set; }
    }
}
