using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("VIPHAMSV")]
    public class ViPhamSV
    {
        [Column("VIPHAMID")]
        public int Id { get; set; }

        public string MSSV { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public string MucDoVP { get; set; }

        public string HinhThucXuLy { get; set; }
        
        public int LichThiId { get; set; }

        [ForeignKey("LichThiId")]
        public virtual LichThi LichThi { get; set; }

        public int LopSVId { get; set; }

        [ForeignKey("LopSVId")]
        public virtual LopSinhVien LopSV { get; set; }
    }
}
