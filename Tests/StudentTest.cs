using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Registrar.Objects;

namespace Registrar
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Save_AddOneStudentToDatabase_1()
    {
      //Arrange
      DateTime enrollmentDate = new DateTime(2016, 12, 13);
      Student newStudent = new Student("Brian", enrollmentDate);

      //Act
      newStudent.Save();
      List<Student> allStudents = Student.GetAll();

      //Assert
      Assert.Equal(1, allStudents.Count);
    }
    [Fact]
    public void Find_SelectOneStudentFromDB_Equals()
    {
      //Arrange
      DateTime enrollmentDate = new DateTime(2016, 12, 13);
      Student newStudent = new Student("Brian", enrollmentDate);

      //Act
      newStudent.Save();
      Student foundStudent = Student.Find(newStudent.GetId());

      //Assert
      Assert.Equal(newStudent, foundStudent);
    }
    [Fact]
    public void Find_MultipleStudentsInDatabase_Equals()
    {
      //Arrange
      DateTime enrollmentDate = new DateTime(2016, 12, 13);
      Student student1 = new Student("Brian", enrollmentDate);
      student1.Save();
      Student student2 = new Student("Levi", enrollmentDate);
      student2.Save();
      //Act
      List<Student> allStudents = new List<Student> {student1, student2};
      List<Student> foundStudents = Student.GetAll();
      //Assert
      Assert.Equal(allStudents, foundStudents);
    }

    [Fact]
    public void DeleteStudent_RemoveOneStudentFromDatabase_0()
    {
      //Arrange
      DateTime enrollmentDate = new DateTime(2016, 12, 13);
      Student newStudent = new Student("Brian", enrollmentDate);

      //Act
      newStudent.Save();
      Student.DeleteStudent(newStudent.GetId());
      List<Student> allStudents = Student.GetAll();

      //Assert
      Assert.Equal(0, allStudents.Count);
    }
  
    public void Dispose()
    {
      Student.DeleteAll();
    }
  }
}
