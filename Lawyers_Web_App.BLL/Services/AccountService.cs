using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.AccountDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.DAL.Entities;
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
                    Email = userDTO.Email
                });
                _database.Save();
            }
            else
            {
                throw new ValidationException("Пользователь с таким логином уже зарегистрирован!", "");
            }
        }

        public void RegisterClient(ClientDTO clientDTO)
        {
            ClientProfile client = _database.ClientProfiles.Find(c => c.Name == clientDTO.Name &&
            c.Surname == clientDTO.Surname && c.Patronymic == clientDTO.Patronymic && c.DateOfBirth == clientDTO.DateOfBirth).FirstOrDefault();
            if (client == null)
            {
                _database.ClientProfiles.Create(new ClientProfile
                {
                    Name = clientDTO.Name,
                    Surname = clientDTO.Surname,
                    Patronymic = clientDTO.Patronymic,
                    DateOfBirth = clientDTO.DateOfBirth,
                    Phone = clientDTO.Phone,
                    Email = clientDTO.Email
                });
                _database.Save();
            }
            else
            {
                throw new ValidationException("Клиент уже зарегистрирован!", "");
            }
        }
    }
}
