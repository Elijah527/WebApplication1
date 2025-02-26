using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private static List<Course> courses = new List<Course>();

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            var course = courses.Find(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public ActionResult<Course> CreateCourse(Course course)
        {
            course.Id = courses.Count + 1;
            courses.Add(course);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, Course updatedCourse)
        {
            var course = courses.Find(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            course.Name = updatedCourse.Name;
            course.Students = updatedCourse.Students;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = courses.Find(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            courses.Remove(course);
            return NoContent();
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Student>? Students { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
