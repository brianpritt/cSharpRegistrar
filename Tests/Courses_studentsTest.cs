using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Registrar.Objects;

namespace Registrar
{
  public class CoursesStudentsTest : IDisposable
  {
    public CoursesStudentsTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void AddToCourse_AddsStudentToCourse_Equals()
    {
      //Arrange
      DateTime enrollmentDate = new DateTime(2016, 12, 13);
      Student student1 = new Student("Brian", enrollmentDate);
      student1.Save();
      Course course1 = new Course("Cultural Anthropology", "HIST103");
      course1.Save();
      //Act
      student1.AddToCourse(course1.GetId());
      List<Course> studentCourses = student1.GetCourses();
      //Arrange
      Assert.Equal(1, studentCourses.Count);
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
  }
}
