using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models
{
    public class DocViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int SomethingId { get; set; }
        [Display(Name= "Выберете файл для загрузки")]
        public IFormFile UploadedFile { get; set; }
        public DateTime? Date { get; set; }
    }
}
