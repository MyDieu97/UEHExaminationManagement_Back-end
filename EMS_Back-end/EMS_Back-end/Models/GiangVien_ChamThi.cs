using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("GIANGVIEN_CHAMTHI")]
    public class GiangVien_ChamThi
    {
        [Column("CHAMTHIID")]
        public int Id { get; set; }

        public int SoBaiTL { get; set; }

        public int SoBaiQT { get; set; }

        public int SoBaiKTHP { get; set; }

        [Column("DGTL")]
        public decimal DonGiaTL { get; set; }

        [Column("DGQT")]
        public decimal DonGiaQT { get; set; }

        [Column("DGKTHP")]
        public decimal DonGiaKTHP { get; set; }

        [Column("GVID")]
        public int GiangVienId { get; set; }

        [ForeignKey("GiangVienId")]
        public virtual GiangVien GiangVien { get; set; }

        public int LopHPId { get; set; }

        [ForeignKey("LopHPId")]
        public virtual LopHocPhan LopHocPhan { get; set; }
    }
}
