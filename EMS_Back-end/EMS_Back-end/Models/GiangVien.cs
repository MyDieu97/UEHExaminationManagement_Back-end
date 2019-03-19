using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("GIANGVIEN")]
    public class GiangVien
    {
        [Column("GVID")]
        public int Id { get; set; }

        public string MaGV { get; set; }

        public string HoGV { get; set; }

        public string TenGV { get; set; }

        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        public string SoDT { get; set; }

        public string Email { get; set; }

        public bool HuuTri { get; set; }

        public string GhiChu { get; set; }

        public int DonViId { get; set; }

        [ForeignKey("DonViId")]
        public virtual DonVi DonViTrucThuoc { get; set; }
    }
}
