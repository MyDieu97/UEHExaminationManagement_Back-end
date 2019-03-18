using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("LICHTHI")]
    public class LichThi
    {
        [Column("LICHTHIID")]
        public int Id { get; set; }

        [Column("NGAYGIOTHI")]
        public DateTime NgayGioBDThi { get; set; }

        public string Thu { get; set; }

        public string CSThi { get; set; }

        public string PhongThi { get; set; }

        public int SoSV { get; set; }

        public string HinhThucThi { get; set; }

        public int LopHPId { get; set; }
        
        [ForeignKey("LopHPId")]
        public virtual LopHocPhan LopHP { get; set; }

        public int PhieuDiemId { get; set; }

        [ForeignKey("PhieuDiemId")]
        public virtual PhieuGiaoBangDiem PhieuDiem { get; set; }

        public int PhieuBaiThiId { get; set; }

        [ForeignKey("PhieuBaiThiId")]
        public virtual PhieuGiaoBaiThi PhieuBaiThi { get; set; }
        
        //public int LopSVId { get; set; }

        //[ForeignKey("LopSVId")]
        //public virtual LopSinhVien LopSV { get; set; }
        
    }
}
