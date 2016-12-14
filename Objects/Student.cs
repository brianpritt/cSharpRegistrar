using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Registrar.Objects
{
  public class Student
  {
    private string _name;
    private DateTime _enrollmentDate;
    private int _id;

    public Student(string name, DateTime enrollmentDate, int id = 0)
    {
      _name = name;
      _enrollmentDate = enrollmentDate;
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
    public DateTime GetEnrollmentDate()
    {
      return _enrollmentDate;
    }

    public override bool Equals(Object otherStudent)
    {
      if(!(otherStudent is Student)) return false;
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = _id == newStudent.GetId();
        bool nameEquality = _name == newStudent.GetName();
        bool enrollmentEquality = _enrollmentDate == newStudent.GetEnrollmentDate();

        return(idEquality && nameEquality && enrollmentEquality);
      }
    }

    public override int GetHashCode()
    {
      return _name.GetHashCode();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO students (name, enrollment_date) OUTPUT INSERTED.id VALUES (@Name, @EnrollmentDate);", conn);
      cmd.Parameters.AddWithValue("@Name", _name);
      cmd.Parameters.AddWithValue("@EnrollmentDate", _enrollmentDate);
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    public static List<Student> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("SELECT * FROM students;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      List<Student> allStudents = new List<Student>{};

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentEnrollmentDate = rdr.GetDateTime(2);

        Student newStudent = new Student(studentName, studentEnrollmentDate, studentId);
        allStudents.Add(newStudent);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return allStudents;
    }

    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT name, enrollment_date FROM students WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      string name = null;
      DateTime enrollmentDate = DateTime.Today;
      while (rdr.Read())
      {
        name = rdr.GetString(0);
        enrollmentDate = rdr.GetDateTime(1);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return new Student(name, enrollmentDate, id);
    }

    public void AddToCourse(int course_id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO courses_students (course_id, student_id) VALUES (@CourseId, @StudentId);", conn);
      cmd.Parameters.AddWithValue("@CourseId", course_id);
      cmd.Parameters.AddWithValue("@StudentId", _id);
      cmd.ExecuteNonQuery();
      if (conn != null) conn.Close();
    }

    public List<Course> GetCourses()
    {
      List<Course> studentCourses = new List<Course> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students JOIN courses_students ON (students.id = courses_students.student_id) JOIN courses ON (courses_students.course_id = courses.id) WHERE students.id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", _id);
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        string courseDescription = rdr.GetString(3);
        Course foundCourse = new Course(courseName, courseNumber, courseDescription, courseId);
        studentCourses.Add(foundCourse);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return studentCourses;
    }

    public List<Course> GetAvailableCourses()
    {
      List<Course> availableCourses = new List<Course> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM courses JOIN courses_students ON courses.id = courses_students.course_id WHERE courses_students.student_id != @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", _id);
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        string courseDescription = rdr.GetString(3);
        Course foundCourse = new Course(courseName, courseNumber, courseDescription, courseId);
        availableCourses.Add(foundCourse);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return availableCourses;
    }

    public static void DeleteStudent(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id = @Id;", conn);
      cmd.Parameters.AddWithValue("@Id", id);
      cmd.ExecuteNonQuery();
      if (conn != null) conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students; DELETE FROM courses_students", conn);
      cmd.ExecuteNonQuery();
      if (conn != null) conn.Close();
    }
  }
}
//
