using Lawyers_Web_App.BLL.DTO.OtherDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Other
{
    public interface IQuestionAndCommentService<T> 
        where T:class
    {
        void Add(T item);
        IEnumerable<T> GetAll();
        T Get(int id);
        void Delete(int id);
        void Dispose();
    }
}
