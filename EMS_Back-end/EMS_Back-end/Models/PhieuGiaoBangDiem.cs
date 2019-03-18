﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class PhieuGiaoBangDiem
    {
        public int Id { get; set; }

        public string MaPhieu { get; set; }

        public DateTime NgayGiao { get; set; }

        public int DonViId { get; set; }

        [ForeignKey("DonViId")]
        public virtual DonVi DonVi { get; set; }
    }
}
