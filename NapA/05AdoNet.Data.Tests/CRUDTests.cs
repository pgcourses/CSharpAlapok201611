using System;
//using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _05AdoNet.Data.Tests
{
    [TestClass]
    public class CRUDTests
    {

        [TestMethod]
        public void TeacherList()
        {
            //Arrange
            var db = new DbAccess();

            //Act
            var teachers = db.GetTeachers();

            //Assert
            Assert.AreEqual(2, teachers.Count);

        }


        [TestMethod]
        public void TeacherCreate()
        {
            //Arrange
            var db = new DbAccess();

            var teacher = new Teacher()
            {
                FirstName = "Teszt"
                ,LastName = "Elek"
                ,ClassCode = "3/A"
                ,Subject_Id = 1
            };

            //Act
            var id = db.CreateTeacher(teacher);

            //Assert
            Assert.AreNotEqual(0, id);

            //Tear down
            var teacherSaved = db.ReadTeacher(id);
            Assert.IsNotNull(teacherSaved);

            Assert.AreEqual(teacher.FirstName, teacherSaved.FirstName);
            Assert.AreEqual(teacher.LastName, teacherSaved.LastName);
            Assert.AreEqual(teacher.ClassCode, teacherSaved.ClassCode);
            Assert.AreEqual(teacher.Subject_Id, teacherSaved.Subject_Id);

            var deletedRecords = db.DeleteTeacher(id);
            Assert.AreEqual(1, deletedRecords);

        }

        [TestMethod]
        public void TeacherRead()
        {
            //Házi feladat
        }

        [TestMethod]
        public void TeacherUpdate()
        {

        }

        [TestMethod]
        public void TeacherDelete()
        {
            //Házi feladat, figyelem: törlés után visszaállítás
        }

    }
}
