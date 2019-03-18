using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class GiangVien_ChamThi
    {
        public int Id { get; set; }

        public int SoBaiTL { get; set; }

        public int SoBaiQT { get; set; }

        public int SoBaiKTHP { get; set; }

        public decimal DonGiaTL { get; set; }

        public decimal DonGiaQT { get; set; }

        public decimal DonGiaKTHP { get; set; }

        public int GiangVienId { get; set; }

        [ForeignKey("GiangVienId")]
        public virtual GiangVien GiangVien { get; set; }

        public int LopHPId { get; set; }

        [ForeignKey("LopHPId")]
        public virtual LopHocPhan LopHocPhan { get; set; }
    }
}
