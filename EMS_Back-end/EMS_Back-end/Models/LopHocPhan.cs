﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class LopHocPhan
    {
        public int Id { get; set; }

        public string MaLopHP { get; set; }

        public string ThoiKB { get; set; }

        public int HocPhanId { get; set; }

        [ForeignKey("HocPhanId")]
        public virtual HocPhan HocPhan { get; set; }

        public int LopSVId { get; set; }

        [ForeignKey("LopSVId")]
        public virtual LopSinhVien LopSV { get; set; }
    }
}
