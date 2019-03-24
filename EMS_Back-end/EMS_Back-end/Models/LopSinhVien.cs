using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("LOPSV")]
    public class LopSinhVien
    {
        [Column("LOPID")]
        public int Id { get; set; }

        public string MaLop { get; set; }

        public string NganhHoc { get; set; }

        public string Khoa { get; set; }
    }
}
