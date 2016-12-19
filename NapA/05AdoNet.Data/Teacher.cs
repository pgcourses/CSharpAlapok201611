
using System.ComponentModel.DataAnnotations; //ez a MaxLength-hez kell

namespace _05AdoNet.Data
{
    public class Teacher
    {
        public int Id { get; set; }

        //[MaxLength(50)] //Ha indexelni akarunk, akkor a string típusnál meg kell adnunk a hosszat is!
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClassCode { get; set; }
        public int Subject_Id { get; set; }
    }
}