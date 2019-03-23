using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("LICHTHI")]
    public class LichThi
    {
        [Column("LICHTHIID")]
        public int Id { get; set; }        

        public string PhongThi { get; set; }

        public int SoSV { get; set; }

        public int LopHPId { get; set; }
        
        [ForeignKey("LopHPId")]
        public virtual LopHocPhan LopHP { get; set; }                
    }
}
