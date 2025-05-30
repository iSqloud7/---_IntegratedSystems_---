namespace CoursesApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ECTS { get; set; }
        public string SemesterType { get; set; }
    }
}
