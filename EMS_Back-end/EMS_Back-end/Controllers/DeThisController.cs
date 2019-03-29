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

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> GetDeThis(DeThiRequest request)
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
                string path = _hostingEnvironment.WebRootPath + "\\Forms\\FormDeThi.xlsx";
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
                //FileInfo excelFile = new FileInfo(@"F:\Data.xlsx");
                //file.SaveAs(excelFile);
                //byte[] excelData = file.GetAsByteArray();

                //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                //var memoryStream = new MemoryStream(excelData);

                //result.Content = new ByteArrayContent(memoryStream.GetBuffer());
                //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                //{
                //    FileName = "Data.xlsx"
                //};
                //return result;

                //return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Data.xlsx");

                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    file.SaveAs(memoryStream);

                //    var result = new HttpResponseMessage(HttpStatusCode.OK)
                //    {
                //        Content = new ByteArrayContent(memoryStream.ToArray())
                //    };
                //    result.Content.Headers.ContentDisposition =
                //        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                //        {
                //            FileName = "Data.xlsx"
                //        };
                //    result.Content.Headers.ContentType =
                //        new MediaTypeHeaderValue("application/octet-stream");

                //    return result;
                //}
            }
        }
    }
}