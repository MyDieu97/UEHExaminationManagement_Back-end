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

        public string PhongThi { get; set; }

        public int SoSV { get; set; }

        public int LopHPId { get; set; }
        
        [ForeignKey("LopHPId")]
        public virtual LopHocPhan LopHP { get; set; }                
    }

    public  class LichThiInfo
    {
        public int Id { get; set; }

        public string PhongThi { get; set; }

        public int SoSV { get; set; }

        public string MaLopHP { get; set; }

        public string ThoiKB { get; set; }

        public DateTime NgayGioBDThi { get; set; }

        public string Thu { get; set; }

        public string CSThi { get; set; }

        public string MaHP { get; set; }

        public string TenHP { get; set; }

        public int SoTinChi { get; set; }

        public string BacDaoTao { get; set; }

        public string HeDaoTao { get; set; }

        public string MaLopSV { get; set; }

        public string NganhHoc { get; set; }

        public string Khoa { get; set; }
    }
}
