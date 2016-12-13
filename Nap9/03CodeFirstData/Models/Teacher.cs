using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03CodeFirstData.Models
{

    /// <summary>
    /// Egyszerűsítés: minden tanár egy adott osztályban egy tantárgyat tanít
    /// </summary>

    public class Teacher
    {
        public int Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string ClassCode { get; set; }

    }
}
