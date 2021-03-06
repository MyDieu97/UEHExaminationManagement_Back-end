﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("HOCPHAN")]
    public class HocPhan
    {
        [Column("HOCPHANID")]
        public int Id { get; set; }

        public string MaHP { get; set; }

        public string TenHP { get; set; }

        public int SoTinChi { get; set; }

        public int DonViRaDeId { get; set; }

        public int DonViQuanLyId { get; set; }

        [ForeignKey("DonViRaDeId")]
        public virtual DonVi DonViRaDe { get; set; }

        [ForeignKey("DonViQuanLyId")]
        public virtual DonVi DonViQuanLy { get; set; }
    }

    public class HocPhanInfo
    {
        public int Id { get; set; }

        public string MaHP { get; set; }

        public string TenHP { get; set; }

        public int SoTinChi { get; set; }

        public string DonViRaDe { get; set; }

        public string DonViQuanLy { get; set; }
    }
}
