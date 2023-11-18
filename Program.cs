using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    public int ID { get; set; }

    public string Name { get; set; }

    public int Year { get; set; }
}

using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your database connection here
        optionsBuilder.UseSqlServer("YourConnectionString");
    }
}

class Program
{
    static void Main()
    {
        using (var context = new SchoolContext())
        {
            // Create the database (if not exists)
            context.Database.EnsureCreated();

            // Add students
            AddStudent(context, "John Doe", 2023);
            AddStudent(context, "Jane Smith", 2022);
            AddStudent(context, "Bob Johnson", 2024);
            AddStudent(context, "Alice Williams", 2021);

            // Query and display all students
            DisplayAllStudents(context);
        }
    }

    static void AddStudent(SchoolContext context, string name, int year)
    {
        var newStudent = new Student { Name = name, Year = year };
        context.Students.Add(newStudent);
        context.SaveChanges();
        Console.WriteLine($"Added student: ID: {newStudent.ID}, Name: {newStudent.Name}, Year: {newStudent.Year}");
    }

    static void DisplayAllStudents(SchoolContext context)
    {
        var students = context.Students.ToList();
        Console.WriteLine("All Students:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Year: {student.Year}");
        }
    }
}
