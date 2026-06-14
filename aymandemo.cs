using System;

class Program
{
    static void Main()
    {
        int age = -5; // Invalid age

        if (age > 18)
        {
            Console.WriteLine("Adult");
        }
        else if (age < 18)
        {
            Console.WriteLine("Minor");
        }

        // Missing check for age == 18

        int number = 10;
        int result = number / 0; // Runtime error: Divide by zero

        Console.WriteLine(result);
    }
}
