using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("GIANGVIEN_COITHI")]
    public class GiangVien_CoiThi
    {
        [Column("COITHIID")]
        public int Id { get; set; }

        public byte GiamThi { get; set; }

        [Column("GVID")]
        public int GiangVienId { get; set; }

        [ForeignKey("GiangVienId")]
        public virtual GiangVien GiangVien { get; set; }

        public int LichThiId { get; set; }

        [ForeignKey("LichThiId")]
        public virtual LichThi LichThi { get; set; }
    }
}
