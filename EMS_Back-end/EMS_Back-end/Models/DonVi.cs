using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("DONVI")]
    public class DonVi
    {
        [Column("DONVIID")]
        public int Id { get; set; }

        public string MaDonVi { get; set; }

        public string TenDonVi { get; set; }

        public string SoDT { get; set; }

        public string Email { get; set; }
    }
}
