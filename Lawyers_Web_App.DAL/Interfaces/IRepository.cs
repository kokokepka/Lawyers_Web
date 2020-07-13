using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Interfaces
{
    // логика взаимодействия с БД, для работы с БД (используем паттерн Репозиторий)
    public interface IRepository<T> where T : class
    {
        // вся таблица с данными
        IEnumerable<T> GetAll();

        // получение
        T Get(int id);

        // поиск по критерию
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        
        // создание и добавление 
        void Create(T item);

        // обновление 
        void Update(T item);

        // удаление
        void Delete(int id);
    }
}
