using EMS_Back_end.Models;
using EMS_Back_end.Models.Requests;
using EMS_Back_end.Models.Responses;
using EMS_Back_end.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EMS_Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeThisController : ControllerBase
    {
        private readonly Context _context;
        private readonly HostingEnvironment _hostingEnvironment;

        public DeThisController(Context context, HostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("getdethisbydonvi")]
        public async Task<ActionResult<BaseResponse>> GetDeThisbyDonVi(DeThiRequest request)
        {
            var query = await _context.LopHocPhans.Include(x => x.HocPhan)
                                                  .Include(x => x.HocPhan.DonViRaDe)
                                                  .Where(x => x.NgayGioBDThi >= request.NgayBatDau && x.NgayGioBDThi <= request.NgayKetThuc)
                                                  .Where(x => x.HocPhan.DonViRaDeId == request.DonViId)
                                                  .Select(x => new DeThi
                                                  {
                                                      Id = x.Id,
                                                      MaLopHP = x.MaLopHP,
                                                      TenHocPhan = x.HocPhan.TenHP,
                                                      NgayGioThi = x.NgayGioBDThi,
                                                      DonVi = x.HocPhan.DonViRaDe.TenDonVi
                                                  })
                                                  .ToListAsync();
            return new BaseResponse
            {
                Message = "Lấy danh sách thành công",
                Data = query
            };
        }

        [HttpPost("getdethis")]
        public async Task<ActionResult<BaseResponse>> GetDeThis(DeThiRequest request)
        {
            var query = await _context.LichThis.Include(x => x.LopHP.HocPhan)
                                                  .Include(x => x.LopHP.LopSV)
                                                  .Where(x => x.LopHP.NgayGioBDThi >= request.NgayBatDau && x.LopHP.NgayGioBDThi <= request.NgayKetThuc)
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
                                                  })
                                                  .ToListAsync();
            return new BaseResponse
            {
                Message = "Lấy danh sách thành công",
                Data = query
            };
        }

        [HttpPost("sendemail/{giangVienId}")]
        public async Task<ActionResult<BaseResponse>> ExportExcelFile(DeThiRequest request, int giangVienId = 0)
        {
            var list = await _context.LopHocPhans.Include(x => x.HocPhan)
                                           .Include(x => x.HocPhan.DonViQuanLy)
                                           .Include(x => x.HocPhan.DonViRaDe)
                                           .Where(x => x.NgayGioBDThi >= request.NgayBatDau && x.NgayGioBDThi <= request.NgayKetThuc)
                                           .Where(x => x.HocPhan.DonViRaDeId == request.DonViId)
                                           .ToListAsync();
            using (var file = new OfficeOpenXml.ExcelPackage())
            {
                string path = _hostingEnvironment.WebRootPath + "\\Forms\\FormCungCapDeThi.xlsx";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    file.Load(stream);
                }
                var worksheet = file.Workbook.Worksheets.First();
                worksheet.Cells[9, 5].Value = list[0].HocPhan.DonViQuanLy.TenDonVi;
                worksheet.Cells[7, 5].Value = request.NgayBatDau.ToString("dd-MM-yyyy");
                worksheet.Cells[7, 9].Value = request.NgayKetThuc.ToString("dd-MM-yyyy");
                for (int i = 0; i < list.Count - 1; i++)
                {
                    worksheet.InsertRow(i + 16, 1);
                    worksheet.Cells[15, 1, 15, worksheet.Dimension.End.Column].Copy(worksheet.Cells[i + 16, 1, i + 16, worksheet.Dimension.End.Column]);
                    worksheet.Row(i + 16).StyleID = worksheet.Row(15).StyleID;
                }
                string hoTen, email;
                if (giangVienId > 0)
                {
                    var giangVien = _context.GiangViens.Find(giangVienId);
                    hoTen = giangVien.HoGV + " " + giangVien.TenGV;
                    email = giangVien.Email;
                }
                else
                {
                    hoTen = list[0].HocPhan.DonViRaDe.TenDonVi;
                    email = list[0].HocPhan.DonViRaDe.Email;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    var rowCells = worksheet.Cells[i + 15, 1, i + 15, worksheet.Dimension.End.Column];
                    var data = list[i];
                    rowCells[i + 15, 1].Value = i + 1;
                    rowCells[i + 15, 2].Value = hoTen;
                    rowCells[i + 15, 5].Value = data.HocPhan.TenHP;
                    rowCells[i + 15, 8].Value = data.MaLopHP;
                    rowCells[i + 15, 10].Value = data.NgayGioBDThi.ToString("dd-MM-yyyy") + " " + data.NgayGioBDThi.ToString("HH:mm");

                }
                byte[] excelData = file.GetAsByteArray();
                var memoryStream = new MemoryStream(excelData);
                await EmailServiceNew.SendEmail(new IdentityMessage() { Destination = "mydieupt@gmail.com", Subject = "Đề nghị gửi đề thi", Body = "" }, memoryStream);

                return new BaseResponse
                {
                    Message = "Gửi mail thành công"
                };                
            }
        }

        [HttpPost("downloadfile")]
        public async Task<IActionResult> GetFormDuyetDeThi(DeThiRequest request)
        {
            var list = await _context.LichThis.Include(x => x.LopHP)
                                           .Include(x => x.LopHP.HocPhan)
                                           .Include(x => x.LopHP.LopSV)
                                           .Where(x => x.LopHP.NgayGioBDThi >= request.NgayBatDau && x.LopHP.NgayGioBDThi <= request.NgayKetThuc)
                                           .ToListAsync();
            using (var file = new OfficeOpenXml.ExcelPackage())
            {
                string path = _hostingEnvironment.WebRootPath + "\\Forms\\FormDuyetDeThi.xlsx";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    file.Load(stream);
                }
                var worksheet = file.Workbook.Worksheets.First();
                var n = list.Count;
                for (int i = 0; i < n - 1; i++)
                {
                    worksheet.InsertRow(i + 6, 1);
                    worksheet.Cells[5, 1, 5, worksheet.Dimension.End.Column].Copy(worksheet.Cells[i + 6, 1, i + 6, worksheet.Dimension.End.Column]);
                    worksheet.Row(i + 6).StyleID = worksheet.Row(5).StyleID;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    var rowCells = worksheet.Cells[i + 15, 1, i + 15, worksheet.Dimension.End.Column];
                    var data = list[i];
                    string he;
                    switch(data.LopHP.HeDaoTao)
                    {
                        case "Chính quy":
                            he = "CQ";
                            break;
                        case "Vừa học vừa làm":
                            he = "VHVL";
                            break;
                        case "Văn bằng 2":
                            he = "VB2";
                            break;
                        case "Liên thông":
                            he = "LT";
                            break;
                        default:
                            he = "";
                            break;
                            
                    }
                    rowCells[i + 5, 2].Value = i + 1;
                    rowCells[i + 5, 3].Value = data.LopHP.HocPhan.TenHP + "_" + data.LopHP.ThoiKB + "_" + data.LopHP.HocPhan.SoTinChi;
                    rowCells[i + 5, 4].Value = data.LopHP.LopSV.Khoa.Substring(5) + "_" +  data.LopHP.MaLopHP.Substring(4) + "_" + data.LopHP.LopSV.MaLop + "_" + he;
                    rowCells[i + 5, 5].Value = data.LopHP.NgayGioBDThi.ToString("dd-MM-yyyy") + " " + data.LopHP.NgayGioBDThi.ToString("HH:mm");
                    rowCells[i + 5, 7].Value = data.LopHP.CSThi;
                    rowCells[i + 5, 8].Value = data.PhongThi;
                    rowCells[i + 5, 9].Value = data.SoSV;

                }
                byte[] excelData = file.GetAsByteArray();

                var memoryStream = new MemoryStream(excelData);

                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Data.xlsx");
            }
        }
    }
}