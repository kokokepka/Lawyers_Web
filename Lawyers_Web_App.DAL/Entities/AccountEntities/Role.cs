using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Design { get; set; }

        public virtual IList<User> Users { get; set; }
    }
}
