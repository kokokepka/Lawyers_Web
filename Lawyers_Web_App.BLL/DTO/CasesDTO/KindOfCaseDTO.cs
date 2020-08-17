using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.CasesDTO
{
    public class KindOfCaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<InstanceDTO> Instances { get; set; }
    }
}
