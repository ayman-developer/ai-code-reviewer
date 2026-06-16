using System;
using System.Threading.Tasks;

public class UserManager
{
    private string userName;

    public void SaveUser()
    {
        // Null handling issue
        Console.WriteLine(userName.Length);

        // Async issue
        SendEmailAsync().Wait();

        // Multiple responsibilities (SOLID violation)
        Console.WriteLine("Saving user to database...");
        Console.WriteLine("Sending email...");
        Console.WriteLine("Generating report...");
    }

    public async Task SendEmailAsync()
    {
        // Async method with no await
        Console.WriteLine("Email sent");
    }
}

public class Program
{
    public static void Main()
    {
        UserManager manager = new UserManager();
        manager.SaveUser();
    }
}
