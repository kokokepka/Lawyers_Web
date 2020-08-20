using Lawyers_Web_App.WEB.Models.Cases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Users
{
    public class ParticipantViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Введите имя")]
        [Display(Name ="Имя")]
        [StringLength(34, MinimumLength = 2, ErrorMessage = "От 2 до 34 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        [StringLength(34, MinimumLength = 2, ErrorMessage = "От 2 до 34 символов")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
        [StringLength(34, MinimumLength = 2, ErrorMessage = "От 2 до 34 символов")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[0-9]{2}\s[0-9]{3}\s[0-9]{2}\s[0-9]{2}", ErrorMessage = "Введите телефон в формате XX XXX XX XX")]
        public string Phone { get; set; }

        [Display(Name = "Номер домашний телефона")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[0-9]{2}\s[0-9]{2}\s[0-9]{2}", ErrorMessage = "Введите телефон в формате XX XX XX")]
        public string HomePhone { get; set; }

        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }
        public int CaseId { get; set; }
        [Required(ErrorMessage = "Роль не выбрана")]
        [Display(Name = "Роль в деле")]
        public int RoleId { get; set; }
        public IEnumerable<RoleCaseModel> Roles { get; set; }
        public int KindCaseId { get; set; }
    }
}
