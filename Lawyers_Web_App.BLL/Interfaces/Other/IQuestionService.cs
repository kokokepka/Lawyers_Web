using Lawyers_Web_App.BLL.DTO.OtherDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Other
{
    public interface IQuestionService
    {
        void AddQuestion(QuestionDTO question);
        IEnumerable<QuestionDTO> AllQuestions();
        QuestionDTO GetQuestion(int id);
        void Delete(int id);
    }
}
