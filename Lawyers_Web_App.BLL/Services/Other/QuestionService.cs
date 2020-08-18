using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services.Other
{
    public class QuestionService : IQuestionService
    {
        IUnitOfWork _database { get; set; }
        public QuestionService(IUnitOfWork database)
        {
            _database = database;
        }
        public void Add(QuestionDTO item)
        {
            if (item == null)
                throw new ValidationException("Модель ровна null", "");
            DateTime date = DateTime.Now;
            Question question = new Question
            {
                Text = item.Text,
                Name = item.Name,
                DateTime = date
            };
            _database.Questions.Create(question);
            _database.Save();
        }

        public void Delete(int id)
        {
            _database.Questions.Delete(id);
            _database.Save();
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public QuestionDTO Get(int id)
        {
            Question question = _database.Questions.Get(id);
            if (question == null)
                throw new ValidationException("Кооментарий не найден", "");
            var map = ObjectMapper.Mapper.Map<QuestionDTO>(question);
            return map;
        }

        public IEnumerable<QuestionDTO> GetAll()
        {
            IEnumerable<Question> questions = _database.Questions.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<QuestionDTO>>(questions);
            return mapped;
        }

        public void AddAnswer(AnswerDTO answer)
        {
            Question question = _database.Questions.Get(answer.QuestionId);
            if (question == null)
                throw new ValidationException("Вопрос не найден", "");
            Answer new_answer = new Answer
            {
                Text = answer.Text,
                DateTime = DateTime.Now,
                Question = question
            };
            _database.Answers.Create(new_answer);
            _database.Save();
        }

        public IEnumerable<AnswerDTO> GetAnswers(int question)
        {
            IEnumerable<Answer> answers = _database.Answers.Find(a => a.QuestionId == question);
            var map = ObjectMapper.Mapper.Map<IEnumerable<AnswerDTO>>(answers);
            return map;
        }
    }
}
