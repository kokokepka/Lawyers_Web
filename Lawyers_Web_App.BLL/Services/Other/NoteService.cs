using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services
{
    public class NoteService : INoteService
    {
        IUnitOfWork _database { get; set; }

        public NoteService(IUnitOfWork database)
        {
            _database = database;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public NoteDTO GetNote(int? id)
        {
            if (id == null)
                throw new ValidationException("Id заметки не найдено", "");
            var note = _database.Notes.Get(id);
            if (note == null)
                throw new ValidationException("Заметка не найдена", "");

            return new NoteDTO { Id = note.Id, Date = note.Date, Time = note.Time, Text = note.Text, IsDone = note.IsDone, UserId = note.UserId };
        }

        public IEnumerable<NoteDTO> GetUserNotes(UserDTO userDto)
        {
            var notes = _database.Notes.Find(n => n.UserId == userDto.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<NoteDTO>>(notes);
            return mapped;
        }

        public void MakeNote(NoteDTO noteDto)
        {
            User user = _database.Users.Get(noteDto.UserId);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            Note _newNote = new Note
            {
                Date = noteDto.Date,
                Time = noteDto.Time,
                Title = noteDto.Title,
                Text = noteDto.Text,
                IsDone = noteDto.IsDone,
                User = user
            };
            _database.Notes.Create(_newNote);
            _database.Save();
        }

        public void DeleteNote(int id)
        {
            Note note = _database.Notes.Get(id);
            if (note == null)
                throw new ValidationException("Запись не найдена", "");
            _database.Notes.Delete(note.Id);
            _database.Save();
        }

        public IEnumerable<NoteDTO> GetUserNotes(int userId)
        {
            User user = _database.Users.Get(userId);
            if (user == null)
                throw new ValidationException("Адвокат не найден", "");
            var notes = _database.Notes.Find(n => n.UserId == user.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<NoteDTO>>(notes);
            return mapped;
        }
    }
}
