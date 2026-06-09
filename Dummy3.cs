using System;

class UserService
{
    public void ProcessUser(string name)
    {
        // No null check
        Console.WriteLine(name.ToUpper());

        // Hardcoded value
        string role = "Admin";

        if (role == "Admin")
        {
            Console.WriteLine("Welcome Admin");
        }
    }
}

class Program
{
    static void Main()
    {
        UserService service = new UserService();

        // Passing null will cause NullReferenceException
        service.ProcessUser(null);
    }
}
