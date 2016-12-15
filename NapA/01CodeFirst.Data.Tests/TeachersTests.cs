using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _01CodeFirst.Data.Models;
using System.Linq;

namespace _01CodeFirst.Data.Tests
{
    [TestClass]
    public class TeachersTests
    {
        [TestMethod]
        public void TeachersTable_ShouldBeEmpty()
        {
            //Arrange
            var db = new SchoolContext();

            //Act
            var count = db.Teachers.Count();

            //Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void AddTeachersToTeachersTable_ShouldBeAppear()
        {
            //Arrange
            var db = new SchoolContext();
            var teacher = new Teacher() { ClassCode = "1/A", Firstname = "Gipsz", Lastname = "Jakab" };
            db.Teachers.Add(teacher); //Itt az Id a integer default értéke == 0.
            db.SaveChanges(); //Itt visszajön az adatbázisból, és megkapjuk a lementett rekord azonosítóját

            //Act

            //Ez megkeresi az elsőt. Ha nincs egy sem, akkor exception-t dob
            //var teacherSaved =  db.Teachers
            //                      .First(x => x.Firstname == teacher.Firstname
            //                                && x.Lastname == teacher.Lastname);

            //Ez megkeresi az elsőt. Ha nincs, null-t ad vissza
            //var teacherSaved = db.Teachers
            //                      .FirstOrDefault(x => x.Firstname == teacher.Firstname
            //                                        && x.Lastname == teacher.Lastname);

            var teacherSaved = db.Teachers
                                  .FirstOrDefault(x => x.Id == teacher.Id);

            //Assert
            Assert.IsNotNull(teacherSaved);
            Assert.AreEqual(teacher.ClassCode, teacherSaved.ClassCode);
            Assert.AreEqual(teacher.Firstname, teacherSaved.Firstname);
            Assert.AreEqual(teacher.Lastname, teacherSaved.Lastname);

            //Teardown
            db.Teachers.Remove(teacherSaved);
            db.SaveChanges();

        }

    }
}
