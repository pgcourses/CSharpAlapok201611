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

            //1   Forgó Morgó   2 / A 1
            //2   Gipsz Jakab   1 / A 2

            var teacher = teachers[0];
            Assert.AreEqual(1, teacher.Id);
            Assert.AreEqual("Forgó", teacher.FirstName);
            Assert.AreEqual("Morgó", teacher.LastName);
            Assert.AreEqual("2/A", teacher.ClassCode);
            Assert.AreEqual(1, teacher.Subject_Id);

            teacher = teachers[1];
            Assert.AreEqual(2, teacher.Id);
            Assert.AreEqual("Gipsz", teacher.FirstName);
            Assert.AreEqual("Jakab", teacher.LastName);
            Assert.AreEqual("1/A", teacher.ClassCode);
            Assert.AreEqual(2, teacher.Subject_Id);

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
            //Arrange
            var db = new DbAccess();
            var teacher = db.ReadTeacher(1);
            //1   Forgó Morgó   2 / A 1
            teacher.FirstName = "Teszt1";
            teacher.LastName = "Teszt2";
            teacher.ClassCode = "8/C";
            teacher.Subject_Id = 2;


            //Act
            var updatedTeachers = db.UpdateTeacher(teacher);

            //Assert
            Assert.AreEqual(1, updatedTeachers);

            var teacherSaved = db.ReadTeacher(1);
            Assert.AreEqual(teacher.FirstName, teacherSaved.FirstName);
            Assert.AreEqual(teacher.LastName, teacherSaved.LastName);
            Assert.AreEqual(teacher.ClassCode, teacherSaved.ClassCode);
            Assert.AreEqual(teacher.Subject_Id, teacherSaved.Subject_Id);

            //TearDown
            //1   Forgó Morgó   2 / A 1
            teacher.FirstName = "Forgó";
            teacher.LastName = "Morgó";
            teacher.ClassCode = "2/A";
            teacher.Subject_Id = 1;

            updatedTeachers = db.UpdateTeacher(teacher);

            Assert.AreEqual(1, updatedTeachers);
            teacherSaved = db.ReadTeacher(1);
            Assert.AreEqual(teacher.FirstName, teacherSaved.FirstName);
            Assert.AreEqual(teacher.LastName, teacherSaved.LastName);
            Assert.AreEqual(teacher.ClassCode, teacherSaved.ClassCode);
            Assert.AreEqual(teacher.Subject_Id, teacherSaved.Subject_Id);



        }

        [TestMethod]
        public void TeacherDelete()
        {
            //Házi feladat, figyelem: törlés után visszaállítás
        }

    }
}
