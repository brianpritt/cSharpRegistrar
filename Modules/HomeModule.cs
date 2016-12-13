using Nancy;
using System.Collections.Generic;
using System;
using Registrar.Objects;
using Nancy.ViewEngines.Razor;

namespace Registrar
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        return View["index.cshtml"];
      };
      Get["/students"] = _ =>
      {
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Get["/students/add_new"] = _ =>
      {
        List<Course> allCourses = Course.GetAll();
        return View["new_student.cshtml", allCourses];
      };
      Post["/students/"] = _ =>
      {
        string name = Request.Form["student-name"];
        DateTime enrollmentDate = (DateTime) Request.Form["date"];
        Student newStudent = new Student(name, enrollmentDate);
        newStudent.Save();
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Get["/students/{id}"] = parameters =>
      {
        Student newStudent = Student.Find(parameters.id);
        return View["student.cshtml", newStudent];
      };
      Delete["/students/remove"] = _ =>
      {
        int studentId = int.Parse(Request.Form["student-id"]);
        Student.DeleteStudent(studentId);
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Get["/courses"] = _ =>
      {
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
      Get["/courses/add_new"] = _ =>
      {
        List<Student> allStudents = Student.GetAll();
        return View["new_course.cshtml", allStudents];
      };
      Post["/courses"] = _ =>
      {
        string name = Request.Form["name"];
        string number = Request.Form["number"];
        string description = Request.Form["description"];
        Course newCourse = new Course(name, number, description);
        newCourse.Save();
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
      Get["/courses/{id}"] = parameters =>
      {
        Course foundCourse = Course.Find(parameters.id);
        return View["course.cshtml", foundCourse];
      };
      Delete["/courses/remove"] = _ =>
      {
        int courseId = int.Parse(Request.Form["course-id"]);
        Course.DeleteCourse(courseId);
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
    }
  }
}
