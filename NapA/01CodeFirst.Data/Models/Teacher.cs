using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01CodeFirst.Data.Models
{
    public class Teacher
    {
        public Teacher()
        { //null object
            Students = new List<Student>();
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ClassCode { get; set; }
        [Required]
        public virtual Subject Subject { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}
