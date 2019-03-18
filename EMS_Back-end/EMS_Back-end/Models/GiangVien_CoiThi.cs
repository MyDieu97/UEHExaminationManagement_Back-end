using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class GiangVien_CoiThi
    {
        public int Id { get; set; }

        public byte GiamThi { get; set; }

        public int GiangVienId { get; set; }

        [ForeignKey("GiangVienId")]
        public virtual GiangVien GiangVien { get; set; }

        public int LichThiId { get; set; }

        [ForeignKey("LichThiId")]
        public virtual LichThi LichThi { get; set; }
    }
}
