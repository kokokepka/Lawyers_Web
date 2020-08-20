using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.AccountDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWork _database { get; set; }

        public AccountService(IUnitOfWork uow)
        {
            _database = uow;
        }
        public void Dispose()
        {
            _database.Dispose();
        }

        public AccountDTO Login(string login, string password)
        {
            string pass = HachPassword.CreateMD5(password);
            User user = _database.Users.Find(u => u.Login == login && u.Password == pass).FirstOrDefault();
            if (user != null)
            {
                return new AccountDTO
                {
                    Id = user.Id,
                    Login = user.Login,
                    RoleId = user.RoleId,
                    Role = user.Role.Design                   
                };
            }
            else 
                return null;
        }

        public void Register(UserDTO userDTO)
        {
            User user = _database.Users.Find(p => p.Login == userDTO.Login).FirstOrDefault();
            if(user == null)
            {
                int roleId = 2;
                _database.Users.Create(new User
                {
                    Login = userDTO.Login,
                    Password = HachPassword.CreateMD5(userDTO.Password),
                    Role = _database.Roles.Get(roleId),
                    Name = userDTO.Name,
                    Surname = userDTO.Surname,
                    Patronymic = userDTO.Patronymic,
                    DateOfBirth = userDTO.DateOfBirth,
                    Phone = userDTO.Phone,
                    HomePhone = userDTO.HomePhone,
                    Email = userDTO.Email,
                    Address = userDTO.Address
                });
                _database.Save();
            }
            else
            {
                throw new ValidationException("Пользователь с таким логином уже зарегистрирован!", "");
            }
        }

        public UserDTO GetUser(string login)
        {
            var user = _database.Users.Find(c => c.Login == login).FirstOrDefault();
            if (user != null)
            {
                var mapped = ObjectMapper.Mapper.Map<UserDTO>(user);
                return mapped;
            }
            else
            {
                throw new ValidationException("Пользователь не найден!", "");
            }
        }

        public void AddUserPhoto(UserDTO userDTO)
        {
            User user = _database.Users.Get(userDTO.Id);
            user.Avatar = userDTO.Avatar;
            _database.Users.Update(user);
            _database.Save();
        }

        public void AddUserDocument(int userId, UserDocDTO userDoc)
        {
            User user = GetUser(userId);
            _database.UserDocuments.Create(new UserDocument { Name = userDoc.Name, Path = userDoc.Path, Date = userDoc.Date, User = user });
            _database.Save();
        }

        public IEnumerable<UserDocDTO> GetUserDocuments(int userId)
        {
            User user = GetUser(userId);
            var _docs = _database.UserDocuments.Find(d => d.UserId == user.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<UserDocDTO>>(_docs);
            return mapped;
        }

        private User GetUser(int Id)
        {
            User user = _database.Users.Get(Id);
            if (user == null)
                throw new ValidationException("Адвокат не найден", "");
            return user;
        }

        public UserDocDTO GetUserDocument(int docId)
        {
            var doc = _database.UserDocuments.Get(docId);
            if (doc == null)
                throw new ValidationException("Документ не найден", "");
            var mapped = ObjectMapper.Mapper.Map<UserDocDTO>(doc);
            return mapped;
        }
        public void DeleteDoc(int Id)
        {
            UserDocument doc = _database.UserDocuments.Get(Id);
            if (doc == null)
                throw new ValidationException("Документ не найден", "");
            _database.UserDocuments.Delete(doc.Id);
            _database.Save();
        }


        public void DeleteUser(int id)
        {
            User user = _database.Users.Get(id);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            _database.Users.Delete(user.Id);
            _database.Save();
        }
        UserDTO IAccountService.GetUser(int id)
        {
            var user = _database.Users.Get(id);
            if (user != null)
            {
                var mapped = ObjectMapper.Mapper.Map<UserDTO>(user);
                return mapped;
            }
            else
            {
                throw new ValidationException("Пользователь не найден!", "");
            }
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            IEnumerable<User> lowyers = _database.Users.GetAll();
            IList<UserDTO> users = new List<UserDTO>();
            foreach (User item in lowyers)
            {
                users.Add(new UserDTO
                {
                    Name = item.Name,
                    Surname = item.Surname,
                    Patronymic = item.Patronymic,
                    Email = item.Email,
                    Phone = item.Phone,
                    Avatar = item.Avatar
                });
            }
            return users;
        }
    }
}
