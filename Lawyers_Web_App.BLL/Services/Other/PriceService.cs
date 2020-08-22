
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services.Other
{
    public class PriceService : IPriceService
    {
        IUnitOfWork _database { get; set; }
        public PriceService(IUnitOfWork database)
        {
            _database = database;
        }
        public IEnumerable<PriceDTO> GetPrices()
        {
            var result = ObjectMapper.Mapper.Map<IEnumerable<PriceDTO>>(_database.Prices.GetAll());
            return result;
        }
    }
}
