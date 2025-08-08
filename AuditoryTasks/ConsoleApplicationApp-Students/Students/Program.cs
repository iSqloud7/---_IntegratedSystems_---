
using ConsoleApp1;

class Program
{
    static void Main()
    {
        try
        {
            var student1 = new UndergraduateStudent(1, "John Doe", 2, "Computer Science");
            var student2 = new MasterStudent(2, "Jane Smith", 3, "Artificial Intelligence");
            var student3 = new PhDStudent(3, "Bob Wilson", 4);

            var math = new Course("MAT101", "Calculus", 6);
            var programming = new Course("CSE102", "Programming", 6);

            student1.EnrollInCourse(math);
            student1.EnrollInCourse(programming);
            student2.EnrollInCourse(math);

            var grade1 = new Grade(student1, math, 6);
            var grade2 = new Grade(student1, programming, 10);
            var grade3 = new Grade(student2, math, 8);

            Console.WriteLine("Student Information:");
            Console.WriteLine(student1);
            Console.WriteLine(student2);

            Console.WriteLine("\nCourse Information:");
            Console.WriteLine(math);
            Console.WriteLine(programming);

            Console.WriteLine("\nTop Performing Student in Math:");
            Console.WriteLine(math.GetTopPerformingStudent());
            
            Console.WriteLine("\nFailing Students in Math:");
            math.GetFailingStudents().ToList().ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\nJohn's Courses with Grade Above 90:");
            foreach (var course in student1.GetCoursesWithGradeAbove(90))
            {
                Console.WriteLine(course);
            }

            var test = new
            {
                Name = student1.Name,
                Major = student1.Id
            };
            
            Console.WriteLine(test);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}