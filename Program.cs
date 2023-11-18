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
        //database server not working on my mac.
        optionsBuilder.UseSqlServer("the-connection-string");
    }
}

class Program
{
    static void Main()
    {
        using (var context = new SchoolContext())
        {
            context.Database.EnsureCreated();

            // Add students
            AddStudent(context, "John Doe", 2023);
            AddStudent(context, "Jane Smith", 2022);
            AddStudent(context, "Bob Johnson", 2024);
            AddStudent(context, "Alice Williams", 2021);

            // Update the second row
            UpdateSecondStudent(context);

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

    static void UpdateSecondStudent(SchoolContext context)
    {
        var secondStudent = context.Students.Skip(1).FirstOrDefault(); // Skip(1) to get the second student
        if (secondStudent != null)
        {
            secondStudent.Year = 2025; // Update the year to 2025
            context.SaveChanges();
            Console.WriteLine($"Updated second student: ID: {secondStudent.ID}, New Year: {secondStudent.Year}");
        }
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

