using System;
using System.Collections.Generic;

namespace SampleApp
{
    // 1. Define a class to represent an Employee
    public class Employee
    {
        // Properties
        public string Name { get; set; }
        public string Role { get; set; }
        public double MonthlySalary { get; set; }

        // Constructor
        public Employee(string name, string role, double monthlySalary)
        {
            Name = name;
            Role = role;
            MonthlySalary = monthlySalary;
        }

        // Method to calculate annual bonus (10% of annual salary)
        public double CalculateBonus()
        {
            return (MonthlySalary * 12) * 0.10;
        }
    }

    // 2. The main program execution
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Employee Management System ===\n");

            // Create a list of Employee objects
            List<Employee> employees = new List<Employee>
            {
                new Employee("Alice Smith", "Manager", 5000),
                new Employee("Bob Jones", "Developer", 4200),
                new Employee("Charlie Brown", "Designer", 3800)
            };

            // Loop through the list and display information
            foreach (var emp in employees)
            {
                double annualBonus = emp.CalculateBonus();
                
                // Using string interpolation ($"") for clean formatting
                Console.WriteLine($"Name:   {emp.Name}");
                Console.WriteLine($"Role:   {emp.Role}");
                Console.WriteLine($"Salary: ${emp.MonthlySalary}/month");
                Console.WriteLine($"Bonus:  ${annualBonus:F2} (Annual)");
                Console.WriteLine(new string('-', 30));
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
