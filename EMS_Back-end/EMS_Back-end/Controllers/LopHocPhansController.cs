using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS_Back_end.Models;
using EMS_Back_end.Models.Responses;

namespace EMS_Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocPhansController : ControllerBase
    {
        private readonly Context _context;

        public LopHocPhansController(Context context)
        {
            _context = context;
        }

        // GET: api/LopHocPhans
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetLopHocPhans()
        {
            var lopHocPhan = await _context.LopHocPhans.Include(x => x.HocPhan)
                                            .Include(x => x.LopSV).AsNoTracking()
                                            .Select(x => new LopHocPhanInfo
                                            {
                                                Id = x.Id,
                                                MaLopHP = x.MaLopHP,
                                                ThoiKB = x.ThoiKB,
                                                NgayGioBDThi = x.NgayGioBDThi,
                                                Thu = x.Thu,
                                                CSThi = x.CSThi,
                                                HinhThucThi = x.HinhThucThi,
                                                MaHP = x.HocPhan.MaHP,
                                                TenHP = x.HocPhan.TenHP,
                                                SoTinChi = x.HocPhan.SoTinChi,
                                                BacDaoTao = x.BacDaoTao,
                                                HeDaoTao = x.HeDaoTao,
                                                LopSV = x.LopSV.MaLop,
                                                NganhHoc = x.LopSV.NganhHoc,
                                                Khoa = x.LopSV.Khoa
                                            }).ToListAsync();

            return new BaseResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = lopHocPhan
            };
        }

        // GET: api/LopHocPhans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetLopHocPhan(int id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);

            if (lopHocPhan == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Không tìm thấy"
                };
            }

            return new BaseResponse
            {
                Message = "Tìm thành công",
                Data = lopHocPhan
            };
        }

        // PUT: api/LopHocPhans/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutLopHocPhan(int id, LopHocPhan lopHocPhan)
        {
            var lopHocPhanSua = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhanSua == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Không tìm thấy"
                };
            }

            lopHocPhanSua.BacDaoTao = lopHocPhan.BacDaoTao;
            lopHocPhanSua.CSThi = lopHocPhan.CSThi;
            lopHocPhanSua.HeDaoTao = lopHocPhan.HeDaoTao;
            lopHocPhanSua.HinhThucThi = lopHocPhan.HinhThucThi;
            lopHocPhanSua.HocPhanId = lopHocPhan.HocPhanId;
            lopHocPhanSua.LopSVId = lopHocPhan.LopSVId;
            lopHocPhanSua.MaLopHP = lopHocPhan.MaLopHP;
            lopHocPhanSua.NgayGioBDThi = lopHocPhan.NgayGioBDThi;
            lopHocPhanSua.PhieuBaiThiId = lopHocPhan.PhieuBaiThiId;
            lopHocPhanSua.PhieuDiemId = lopHocPhan.PhieuDiemId;
            lopHocPhanSua.ThoiKB = lopHocPhan.ThoiKB;
            lopHocPhanSua.Thu = lopHocPhan.Thu;

            _context.LopHocPhans.Update(lopHocPhanSua);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Cập nhật thành công",
                Data = lopHocPhan
            };
        }

        // POST: api/LopHocPhans
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostLopHocPhan(LopHocPhan lopHocPhan)
        {
            try
            {
                _context.LopHocPhans.Add(lopHocPhan);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Thêm mới thành công",
                    Data = lopHocPhan
                };
            }
            catch
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Thêm mới thất bại"
                };
            }
        }

        // DELETE: api/LopHocPhans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteLopHocPhan(int id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Xóa thất bại"
                };
            }

            _context.LopHocPhans.Remove(lopHocPhan);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Xóa thành công"
            };
        }

        private bool LopHocPhanExists(int id)
        {
            return _context.LopHocPhans.Any(e => e.Id == id);
        }
    }
}
