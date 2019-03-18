using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("DONGIACHAMTHI")]
    public class DonGiaChamThi
    {
        public int Id { get; set; }

        public string MaLoai { get; set; }

        public string Loai { get; set; }

        public double DonGia { get; set; }
    }
}
