using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Registrar.Objects;

namespace Registrar
{
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Save_AddOneCourseToDatabase_1()
    {
      //Arrange
      Course newCourse = new Course("Cultural Anthropology", "HIST103");

      //Act
      newCourse.Save();
      List<Course> allCourses = Course.GetAll();

      //Assert
      Assert.Equal(1, allCourses.Count);
    }
    [Fact]
    public void Find_SelectOneCourseFromDB_Equals()
    {
      //Arrange
      Course newCourse = new Course("Cultural Anthropology", "HIST103");

      //Act
      newCourse.Save();
      Course foundCourse = Course.Find(newCourse.GetId());

      //Assert
      Assert.Equal(newCourse, foundCourse);
    }
    [Fact]
    public void Find_MultipleCoursesInDatabase_Equals()
    {
      //Arrange
      Course course1 = new Course("Cultural Anthropology", "HIST103");
      course1.Save();
      Course course2 = new Course("Historical Archaeology", "HIST105");
      course2.Save();
      //Act
      List<Course> allCourses = new List<Course> {course1, course2};
      List<Course> foundCourses = Course.GetAll();
      //Assert
      Assert.Equal(allCourses, foundCourses);
    }

    [Fact]
    public void DeleteCourse_RemoveOneCourseFromDatabase_0()
    {
      //Arrange
      Course newCourse = new Course("Cultural Anthropology", "HIST103");

      //Act
      newCourse.Save();
      Course.DeleteCourse(newCourse.GetId());
      List<Course> allCourses = Course.GetAll();

      //Assert
      Assert.Equal(0, allCourses.Count);
    }
    
    public void Dispose()
    {
      Course.DeleteAll();
    }
  }
}
