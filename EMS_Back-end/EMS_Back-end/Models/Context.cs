using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<LichThi> LichThis { get; set; }
        public virtual DbSet<DonGiaChamThi> DonGiaChamThis { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<HocPhan> HocPhans { get; set; }
        public virtual DbSet<LopHocPhan> LopHocPhans { get; set; }
        public virtual DbSet<LopSinhVien> LopSinhViens { get; set; }
        public virtual DbSet<PhieuGiaoBaiThi> PhieuGiaoBaiThis { get; set; }
        public virtual DbSet<PhieuGiaoBangDiem> PhieuGiaoBangDiems { get; set; }
        public virtual DbSet<ViPhamGV> ViPhamGVs { get; set; }
        public virtual DbSet<ViPhamSV> ViPhamSVs { get; set; }

    }
}
