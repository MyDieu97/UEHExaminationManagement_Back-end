﻿using System;
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
using System.Globalization;

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
        public async Task<ActionResult<BaseResponse>> GetLichThis()
        {
            var lichthis = await _context.LichThis.Include(x => x.LopHP)
                                            .Include(x => x.LopHP.HocPhan)
                                            .Include(x => x.LopHP.LopSV).AsNoTracking()
                                            .Select(x => new LichThiInfo
                                            {
                                                Id = x.Id,
                                                PhongThi = x.PhongThi,
                                                SoSV = x.SoSV,
                                                MaLopHP = x.LopHP.MaLopHP,
                                                ThoiKB = x.LopHP.ThoiKB,
                                                NgayGioBDThi = x.LopHP.NgayGioBDThi,
                                                Thu = x.LopHP.Thu,
                                                CSThi = x.LopHP.CSThi,
                                                MaHP = x.LopHP.HocPhan.MaHP,
                                                TenHP = x.LopHP.HocPhan.TenHP,
                                                SoTinChi = x.LopHP.HocPhan.SoTinChi,
                                                BacDaoTao = x.LopHP.BacDaoTao,
                                                HeDaoTao = x.LopHP.HeDaoTao,
                                                MaLopSV = x.LopHP.LopSV.MaLop,
                                                NganhHoc = x.LopHP.LopSV.NganhHoc,
                                                Khoa = x.LopHP.LopSV.Khoa,
                                            }).ToListAsync();

            return new BaseResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = lichthis
            };
        }

        // GET: api/LichThis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetLichThi(int id)
        {
            var lichThi = await _context.LichThis.FindAsync(id);

            if (lichThi == null)
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
                Data = lichThi
            };
        }

        // PUT: api/LichThis/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutLichThi(int id, LichThi lichThi)
        {
            var lichThiSua = await _context.LichThis.FindAsync(id);
            if (lichThiSua == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Không tìm thấy"
                };
            }

            lichThiSua.LopHPId = lichThi.LopHPId;
            lichThiSua.PhongThi = lichThi.PhongThi;
            lichThiSua.SoSV = lichThi.SoSV;

            _context.LichThis.Update(lichThiSua);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Cập nhật thành công",
                Data = lichThi
            };
        }

        // POST: api/LichThis
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostLichThi(LichThi lichThi)
        {
            try
            {
                _context.LichThis.Add(lichThi);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Thêm mới thành công",
                    Data = lichThi
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

        // DELETE: api/LichThis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteLichThi(int id)
        {
            var lichThi = await _context.LichThis.FindAsync(id);
            if (lichThi == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Xóa thất bại"
                };
            }

            _context.LichThis.Remove(lichThi);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Xóa thành công"
            };
        }

        private bool LichThiExists(int id)
        {
            return _context.LichThis.Any(e => e.Id == id);
        }

        // POST: api/LichThis/ImportFile
        [HttpPost("ImportFile")]
        public async Task<ActionResult<BaseResponse>> ImportFile([FromForm] ImportFileRequest request)
        {
            LichThi lichThi;
            LopHocPhan lopHocPhan;
            LopSinhVien lopSinhVien;
            HocPhan hocPhan;
                        
            using (var file = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = request.File.OpenReadStream())
                {
                    file.Load(stream);
                }
                var worksheet = file.Workbook.Worksheets.First();
                for (int i = 1; i < worksheet.Dimension.End.Row; i++)
                {
                    var rowCells = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                    var maLopSV = rowCells[i, 10].Value.ToString();
                    var khoa = rowCells[i, 9].Value.ToString(); ;
                    lopSinhVien = LopSinhVienExists(maLopSV, khoa);
                    if (lopSinhVien == null)
                    {
                        lopSinhVien = new LopSinhVien();
                        lopSinhVien.MaLop = maLopSV;
                        lopSinhVien.NganhHoc = rowCells[i, 6].Value.ToString();
                        lopSinhVien.Khoa = khoa;
                        await lopSVsCtrl.PostLopSinhVien(lopSinhVien);
                    }
                    var maHP = rowCells[i, 1].Value.ToString();
                    hocPhan = HocPhanExists(maHP);
                    if (hocPhan == null)
                    {
                        hocPhan = new HocPhan();
                        hocPhan.MaHP = maHP;
                        hocPhan.TenHP = rowCells[i, 3].Value.ToString();
                        hocPhan.SoTinChi = Int16.Parse(rowCells[i, 4].Value.ToString());
                        await hocPhansCtrl.PostHocPhan(hocPhan);
                    }
                    var maLopHP = rowCells[i, 2].Value.ToString();
                    lopHocPhan = LopHocPhanExists(maLopHP);
                    if (lopHocPhan == null)
                    {
                        lopHocPhan = new LopHocPhan();
                        lopHocPhan.HocPhanId = hocPhan.Id;
                        lopHocPhan.LopSVId = lopSinhVien.Id;
                        lopHocPhan.MaLopHP = maLopHP;
                        lopHocPhan.CSThi = rowCells[i, 16].Value.ToString();                        
                        var dateStr = rowCells[i, 15].Value.ToString().Substring(0, 10) + " " + rowCells[i, 13].Value.ToString().Replace("g", ":") + ":00";
                        lopHocPhan.NgayGioBDThi = DateTime.Parse(dateStr);
                        lopHocPhan.Thu = rowCells[i, 14].Value.ToString();
                        lopHocPhan.ThoiKB = rowCells[i, 8].Value.ToString();
                        lopHocPhan.HeDaoTao = rowCells[i, 7].Value.ToString();
                        lopHocPhan.BacDaoTao = rowCells[i, 5].Value.ToString();
                        await lopHocPhansCtrl.PostLopHocPhan(lopHocPhan);
                    }
                    var phongThi = rowCells[i, 17].Value.ToString();
                    if (LichThiExists(maLopHP, phongThi) == null)
                    {
                        lichThi = new LichThi();
                        lichThi.LopHPId = lopHocPhan.Id;
                        lichThi.PhongThi = rowCells[i, 17].Value.ToString();
                        lichThi.SoSV = Int16.Parse(rowCells[i, 12].Value.ToString());
                        await PostLichThi(lichThi);
                    }                    
                }

                return new BaseResponse
                {
                    Message = "Import thành công"
                };
            }
        }
                
        private HocPhan HocPhanExists(string maLop)
        {
            return _context.HocPhans.Where(e => e.MaHP == maLop).FirstOrDefault();
        }

        private LopSinhVien LopSinhVienExists(string maLop, string khoa)
        {
            return _context.LopSinhViens.Where(e => e.MaLop == maLop && e.Khoa == khoa).FirstOrDefault();
        }
        
        private LopHocPhan LopHocPhanExists(string maLop)
        {
            return _context.LopHocPhans.Where(e => e.MaLopHP == maLop).FirstOrDefault();
        }

        private LichThi LichThiExists(string maLop, string phongThi)
        {
            return _context.LichThis.Where(e => e.LopHP.MaLopHP == maLop && e.PhongThi == phongThi).FirstOrDefault();
        }
    }

}
