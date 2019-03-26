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
    public class DonVisController : ControllerBase
    {
        private readonly Context _context;

        public DonVisController(Context context)
        {
            _context = context;
        }

        // GET: api/DonVis
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetDonVis()
        {
            return new BaseResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = await _context.DonVis.Where(x => x.Id > 0).ToListAsync()
            };
        }

        // GET: api/DonVis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetDonVi(int id)
        {
            var donVi = await _context.DonVis.FindAsync(id);

            if (donVi == null)
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
                Data = donVi
            };
        }

        // PUT: api/DonVis/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutDonVi(int id, DonVi donVi)
        {
            var donViSua = await _context.DonVis.FindAsync(id);
            if (donViSua == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Không tìm thấy"
                };
            }
                        
            donViSua.Email = donVi.Email;
            donViSua.MaDonVi = donVi.MaDonVi;
            donViSua.TenDonVi = donVi.TenDonVi;
            donViSua.SoDT = donVi.SoDT;

            _context.DonVis.Update(donViSua);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Cập nhật thành công",
                Data = donVi
            };
        }

        // POST: api/DonVis
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostDonVi(DonVi donVi)
        {
            try
            {
                _context.DonVis.Add(donVi);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Thêm mới thành công",
                    Data = donVi
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

        // DELETE: api/DonVis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteDonVi(int id)
        {
            var donVi = await _context.DonVis.FindAsync(id);
            if (donVi == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Xóa thất bại"
                };
            }

            _context.DonVis.Remove(donVi);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Xóa thành công"
            };
        }

        private bool DonViExists(int id)
        {
            return _context.DonVis.Any(e => e.Id == id);
        }
    }
}
