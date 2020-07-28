using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Documents
{
    public interface IDocService<T, N> 
        where T : class 
        where N : class
    {
        void MakeDoc(T docDto);
        T GetDoc(int? id);
        IEnumerable<T> GetAllDocs();
        IEnumerable<T> GetDocs(N userDto);
        void Dispose();
    }
}
