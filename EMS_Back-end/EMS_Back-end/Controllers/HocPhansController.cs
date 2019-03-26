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
    public class HocPhansController : ControllerBase
    {
        private readonly Context _context;

        public HocPhansController(Context context)
        {
            _context = context;
        }

        // GET: api/HocPhans
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetHocPhans()
        {
            var hocphans = await _context.HocPhans.Include(x => x.DonViQuanLy)
                                            .Include(x => x.DonViRaDe).AsNoTracking()
                                            .Select(x => new HocPhanInfo
                                            {
                                                Id = x.Id,                                                
                                                MaHP = x.MaHP,
                                                TenHP = x.TenHP,
                                                SoTinChi = x.SoTinChi,
                                                DonViRaDe = x.DonViRaDe == null ? " " : x.DonViRaDe.TenDonVi,
                                                DonViQuanLy = x.DonViQuanLy == null ? " " : x.DonViQuanLy.TenDonVi
                                            }).ToListAsync();


            return new BaseResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = hocphans
            };
        }

        // GET: api/HocPhans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetHocPhan(int id)
        {
            var hocPhan = await _context.HocPhans.FindAsync(id);

            if (hocPhan == null)
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
                Data = hocPhan
            };
        }

        // PUT: api/HocPhans/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutHocPhan(int id, HocPhan hocPhan)
        {
            var hocPhanSua = await _context.HocPhans.FindAsync(id);
            if (hocPhanSua == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Không tìm thấy"
                };
            }

            hocPhanSua.DonViQuanLyId = hocPhan.DonViQuanLyId;
            hocPhanSua.DonViRaDeId = hocPhan.DonViRaDeId;
            hocPhanSua.MaHP = hocPhan.MaHP;
            hocPhanSua.SoTinChi = hocPhan.SoTinChi;
            hocPhanSua.TenHP = hocPhan.TenHP;

            _context.HocPhans.Update(hocPhanSua);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Cập nhật thành công",
                Data = hocPhan
            };
        }

        // POST: api/HocPhans
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostHocPhan(HocPhan hocPhan)
        {
            try
            {
                _context.HocPhans.Add(hocPhan);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Thêm mới thành công",
                    Data = hocPhan
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

        // DELETE: api/HocPhans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteHocPhan(int id)
        {
            var hocPhan = await _context.HocPhans.FindAsync(id);
            if (hocPhan == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Xóa thất bại"
                };
            }

            _context.HocPhans.Remove(hocPhan);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Xóa thành công"
            };
        }

        private bool HocPhanExists(int id)
        {
            return _context.HocPhans.Any(e => e.Id == id);
        }
    }
}
