using Lawyers_Web_App.BLL.DTO.OtherDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Other
{
    public interface IPriceService
    {
        IEnumerable<PriceDTO> GetPrices();
    }
}
