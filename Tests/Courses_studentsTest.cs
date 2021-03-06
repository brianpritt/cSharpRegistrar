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

    [Fact]
    public void GetAvailableCourses_GetListOfCoursesStudentIsNotEnrolledIn_1()
    {
      //Arrange
      DateTime enrollmentDate = new DateTime(2016, 12, 13);
      Student student1 = new Student("Brian", enrollmentDate);
      student1.Save();
      Course course1 = new Course("Cultural Anthropology", "HIST103");
      course1.Save();
      Course course2 = new Course("History", "HIST101");
      course2.Save();
      //Act
      student1.AddToCourse(course2.GetId());
      List<Course> studentCourses = student1.GetAvailableCourses();
      Console.WriteLine(studentCourses[0].GetName());
      //Arrange
      Assert.Equal(1, studentCourses.Count);
    }

    [Fact]
    public void GetStudents_ReturnsAllStudentsInCourse_Equals()
    {
      //Arrange
      DateTime enrollmentDate = new DateTime(2016, 12, 13);
      Student student1 = new Student("Brian", enrollmentDate);
      student1.Save();
      Course course1 = new Course("Cultural Anthropology", "HIST103");
      course1.Save();
      //Act
      student1.AddToCourse(course1.GetId());
      List<Student> courseStudents = course1.GetStudents();
      //Arrange
      Assert.Equal(1, courseStudents.Count);
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
  }
}
