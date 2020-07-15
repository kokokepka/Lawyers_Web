using Lawyers_Web_App.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces
{
    public interface IDocumentService
    {
        void MakeDoc(DocumentDTO orderDto);
        DocumentDTO GetDoc(int? id);
        IEnumerable<DocumentDTO> GetDocs();
        void Dispose();
    }
}
