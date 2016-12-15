using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04DataFirst
{
    public partial class Teacher
    {
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Firstname, Lastname);
            }
                
        }
    }
}
