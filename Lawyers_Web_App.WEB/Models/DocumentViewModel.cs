using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models
{
    public class DocumentViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int? SomeoneId { get; set; }
        public IFormFile UploadedFile { get; set; }
        public DateTime? Date { get; set; }
    }
}
