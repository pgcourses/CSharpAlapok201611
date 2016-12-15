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

        }

        [TestMethod]
        public void TeacherRead()
        {
        }

        [TestMethod]
        public void TeacherUpdate()
        {
        }

        [TestMethod]
        public void TeacherDelete()
        {
        }

    }
}
