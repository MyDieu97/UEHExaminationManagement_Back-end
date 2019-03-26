using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    [Table("LOPHOCPHAN")]
    public class LopHocPhan
    {
        [Column("LOPHPID")]
        public int Id { get; set; }

        public string MaLopHP { get; set; }

        public string BacDaoTao { get; set; }

        public string HeDaoTao { get; set; }

        [Column("THOIKHOABIEU")]
        public string ThoiKB { get; set; }

        [Column("NGAYGIOTHI")]
        public DateTime NgayGioBDThi { get; set; }

        public string Thu { get; set; }

        public string CSThi { get; set; }

        public string HinhThucThi { get; set; }

        public int HocPhanId { get; set; }

        [ForeignKey("HocPhanId")]
        public virtual HocPhan HocPhan { get; set; }

        public int LopSVId { get; set; }

        [ForeignKey("LopSVId")]
        public virtual LopSinhVien LopSV { get; set; }

        public int PhieuDiemId { get; set; }

        [ForeignKey("PhieuDiemId")]
        public virtual PhieuGiaoBangDiem PhieuDiem { get; set; }

        public int PhieuBaiThiId { get; set; }

        [ForeignKey("PhieuBaiThiId")]
        public virtual PhieuGiaoBaiThi PhieuBaiThi { get; set; }

    }

    public class LopHocPhanInfo
    {
        public int Id { get; set; }

        public string MaLopHP { get; set; }

        public string BacDaoTao { get; set; }

        public string HeDaoTao { get; set; }

        public string ThoiKB { get; set; }

        public DateTime NgayGioBDThi { get; set; }

        public string Thu { get; set; }

        public string CSThi { get; set; }

        public string HinhThucThi { get; set; }

        public string MaHP { get; set; }

        public string TenHP { get; set; }

        public int SoTinChi { get; set; }

        public string LopSV { get; set; }

        public string NganhHoc { get; set; }

        public string Khoa { get; set; }
    }

    public class LopHocPhanQLDiemInfo
    {
        public int Id { get; set; }

        public string MaLopHP { get; set; }

        public string BacDaoTao { get; set; }

        public string HeDaoTao { get; set; }

        public string ThoiKB { get; set; }

        public DateTime NgayGioBDThi { get; set; }

        public string Thu { get; set; }

        public string CSThi { get; set; }

        public string HinhThucThi { get; set; }

        public string MaHP { get; set; }

        public string TenHP { get; set; }

        public int SoTinChi { get; set; }

        public string DonviQuanLy { get; set; }

        public string DonViRaDe { get; set; }

        public string LopSV { get; set; }

        public string NganhHoc { get; set; }

        public string Khoa { get; set; }

        public string MaPhieuDiem { get; set; }

        public string MaPhieuBaiThi { get; set; }
    }

}
