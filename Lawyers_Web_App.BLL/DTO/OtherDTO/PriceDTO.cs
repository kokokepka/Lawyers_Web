using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.OtherDTO
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public string ForWhat { get; set; }
        public double StartSum { get; set; }
        public double FinishSum { get; set; }
    }
}
