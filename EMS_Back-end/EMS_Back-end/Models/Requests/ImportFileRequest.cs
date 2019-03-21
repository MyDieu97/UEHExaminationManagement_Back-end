using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_Back_end.Models.Requests
{
    public class ImportFileRequest
    {
        public IFormFile File { get; set; }
    }
}
