using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Registrar.Objects
{
  public class Course
  {
    private string _name;
    private string _courseNumber;
    private string _description;
    private int _id;

    public Course(string name, string courseNumber, string description = "", int id = 0)
    {
      _name = name;
      _courseNumber = courseNumber;
      _description = description;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetCourseNumber()
    {
      return _courseNumber;
    }
    public string GetDescription()
    {
      return _description;
    }

    public override bool Equals(Object otherCourse)
    {
      if(!(otherCourse is Course)) return false;
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = _id == newCourse.GetId();
        bool nameEquality = _name == newCourse.GetName();
        bool courseEquality = _courseNumber == newCourse.GetCourseNumber();
        bool courseDescription = _description == newCourse.GetDescription();

        return(idEquality && nameEquality && courseEquality && courseDescription);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO courses (name, course_number, course_description) OUTPUT INSERTED.id VALUES (@Name, @CourseNumber, @Description);", conn);
      cmd.Parameters.AddWithValue("@Name", _name);
      cmd.Parameters.AddWithValue("@CourseNumber", _courseNumber);
      cmd.Parameters.AddWithValue("@Description", _description);
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }
    public static List<Course> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("SELECT * FROM courses;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      List<Course> allCourses = new List<Course>{};


      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        string courseDescription = rdr.GetString(3);

        Course newCourse = new Course(courseName,courseNumber, courseDescription, courseId);
        allCourses.Add(newCourse);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return allCourses;
    }

    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT name, course_number, course_description FROM courses WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      string name = null;
      string courseNumber = null;
      string description = null;
      while (rdr.Read())
      {
        name = rdr.GetString(0);
        courseNumber = rdr.GetString(1);
        description = rdr.GetString(2);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return new Course(name, courseNumber, description, id);
    }

    public List<Student> GetStudents()
    {
      SqlConnection conn = DB.Connection();
      conn.Open()
      SqlCommand cmd = new SqlCommand("SELECT students.* FROM courses JOIN courses_students ON (courses.id = courses_students.course_id) JOIN students ON (courses_students.student_id = students.id) WHERE courses.id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", _id);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Student> courseStudents = new List<Student> {}
    }

    public static void DeleteCourse(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM courses WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.ExecuteNonQuery();
      if (conn != null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM courses; DELETE FROM courses_students", conn);
      cmd.ExecuteNonQuery();
      if (conn != null) conn.Close();
    }
  }
}
