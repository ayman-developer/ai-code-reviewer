using System;

// Ask the user for their name
Console.Write("Enter your name: ");

// Read what the user types and store it in a variable
string userName = Console.ReadLine();

// Greet the user using string interpolation ($"...")
Console.WriteLine($"Hello, {userName}! Welcome to C#.");
