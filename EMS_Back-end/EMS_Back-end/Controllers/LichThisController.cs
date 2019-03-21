using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS_Back_end.Models;
using EMS_Back_end.Models.Responses;
using EMS_Back_end.Models.Requests;
using System.IO;

namespace EMS_Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichThisController : ControllerBase
    {
        private readonly Context _context;
        private LopSinhViensController lopSVsCtrl;
        private HocPhansController hocPhansCtrl;
        private LopHocPhansController lopHocPhansCtrl;

        public LichThisController(Context context)
        {
            _context = context;
            lopSVsCtrl = new LopSinhViensController(_context);
            hocPhansCtrl = new HocPhansController(_context);
            lopHocPhansCtrl = new LopHocPhansController(_context);
        }

        // GET: api/LichThis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichThi>>> GetLichThis()
        {
            return await _context.LichThis.ToListAsync();
        }

        // GET: api/LichThis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichThi>> GetLichThi(int id)
        {
            var lichThi = await _context.LichThis.FindAsync(id);

            if (lichThi == null)
            {
                return NotFound();
            }

            return lichThi;
        }

        // PUT: api/LichThis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichThi(int id, LichThi lichThi)
        {
            if (id != lichThi.Id)
            {
                return BadRequest();
            }

            _context.Entry(lichThi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichThiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LichThis
        [HttpPost]
        public async Task<ActionResult<LichThi>> PostLichThi(LichThi lichThi)
        {
            _context.LichThis.Add(lichThi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichThi", new { id = lichThi.Id }, lichThi);
        }

        // DELETE: api/LichThis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LichThi>> DeleteLichThi(int id)
        {
            var lichThi = await _context.LichThis.FindAsync(id);
            if (lichThi == null)
            {
                return NotFound();
            }

            _context.LichThis.Remove(lichThi);
            await _context.SaveChangesAsync();

            return lichThi;
        }

        private bool LichThiExists(int id)
        {
            return _context.LichThis.Any(e => e.Id == id);
        }

        // POST: api/LichThis/ImportFile
        [HttpPost("ImportFile")]
        public async Task<ActionResult<BaseResponse>> ImportFile([FromForm] ImportFileRequest request)
        {
            LichThi lichThi = new LichThi();
            LopHocPhan lopHocPhan = new LopHocPhan();
            LopSinhVien lopSinhVien = new LopSinhVien();
            HocPhan hocPhan = new HocPhan();
                        
            using (var file = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = request.File.OpenReadStream())
                {
                    file.Load(stream);
                }
                var worksheet = file.Workbook.Worksheets.First();
                for (int i = 2; i < 10; i++)
                {
                    var rowCells = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                    var maLopSV = rowCells[i, 10].Value.ToString();
                    if (LopSinhVienExists(maLopSV) == false)
                    {
                        //lopSinhVien.Id = null;
                        lopSinhVien.MaLop = maLopSV;
                        lopSinhVien.NganhHoc = rowCells[i, 6].Value.ToString();
                        lopSinhVien.HeDaoTao = rowCells[i, 5].Value.ToString();
                        //await lopSVsCtrl.PostLopSinhVien(lopSinhVien);
                        _context.LopSinhViens.Add(lopSinhVien);
                    }
                    var maHP = rowCells[i, 1].Value.ToString();
                    if (HocPhanExists(maHP) == false)
                    {
                        hocPhan.HeDaoTao = rowCells[i, 5].Value.ToString();
                        hocPhan.MaHP = maHP;
                        hocPhan.SoTinChi = Int16.Parse(rowCells[i, 4].Value.ToString());
                        //await hocPhansCtrl.PostHocPhan(hocPhan);
                        _context.HocPhans.Add(hocPhan);
                    }
                    var maLopHP = rowCells[i, 2].Value.ToString();
                    if (LopHocPhanExists(maLopHP) == false)
                    {
                        lopHocPhan.HocPhanId = _context.HocPhans.Where(x => x.MaHP == maHP).FirstOrDefault().Id;
                        lopHocPhan.LopSVId = _context.LopSinhViens.Where(x => x.MaLop == maLopSV).FirstOrDefault().Id;
                        lopHocPhan.MaLopHP = maLopHP;
                        lopHocPhan.ThoiKB = rowCells[i, 8].Value.ToString();
                        _context.LopHocPhans.Add(lopHocPhan);
                    }
                    if (LichThiExists(maLopHP) == false)
                    {
                        lichThi.CSThi = rowCells[i, 16].Value.ToString();
                        lichThi.NgayGioBDThi = DateTime.Parse(rowCells[i, 15].Value.ToString());
                        lichThi.PhongThi = rowCells[i, 17].Value.ToString();
                        lichThi.SoSV = Int16.Parse(rowCells[i, 12].Value.ToString());
                        lichThi.Thu = rowCells[i, 14].Value.ToString();
                        //await PostLichThi(lichThi);
                        _context.LichThis.Add(lichThi);
                    }
                    _context.SaveChanges();
                }
            }

            return new BaseResponse
            {
                Message = "Import thành công"
            };
        }

        private bool LopSinhVienExists(string maLop)
        {
            return _context.LopSinhViens.Any(e => e.MaLop == maLop);
        }

        private bool HocPhanExists(string maLop)
        {
            return _context.HocPhans.Any(e => e.MaHP == maLop);
        }

        private bool LopHocPhanExists(string maLop)
        {
            return _context.LopHocPhans.Any(e => e.MaLopHP == maLop);
        }

        private bool LichThiExists(string maLop)
        {
            return _context.LichThis.Any(e => e.LopHP.MaLopHP == maLop);
        }
    }

}
