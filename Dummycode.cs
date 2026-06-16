using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EnterpriseApp.Services
{
    public class PaymentProcessor
    {
        // Flaw 1: Tightly coupled to a concrete class (Dependency Inversion violation)
        private readonly DatabaseContext _dbContext = new DatabaseContext();
        
        // Flaw 2: Doing too much - Business logic, DB access, and Logging (Single Responsibility violation)
        public async Task ProcessUserPaymentsAsync(int userId)
        {
            // Flaw 3: FirstOrDefault can return null. Accessing .Email immediately will cause a NullReferenceException!
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            LogMessage($"Starting payment processing for {user.Email}");

            var pendingInvoices = _dbContext.Invoices.Where(i => i.UserId == userId && !i.IsPaid).ToList();

            foreach (var invoice in pendingInvoices)
            {
                try
                {
                    // Flaw 4: Using .Result on an async method causes thread-blocking (Sync-over-Async deadlocks)
                    var paymentResult = ProcessPaymentExternalAsync(invoice.Amount).Result; 

                    if (paymentResult.Success)
                    {
                        invoice.IsPaid = true;
                        
                        // Flaw 5: Fire-and-forget without 'await'. Exceptions here will be swallowed!
                        NotifyUserAsync(user.Id, invoice.Id); 
                    }
                }
                catch (Exception ex)
                {
                    LogMessage($"Failed to process invoice {invoice.Id}: {ex.Message}");
                }
            }

            // Flaw 6: Using .Wait() instead of await causes more thread blocking
            _dbContext.SaveChangesAsync().Wait();
        }

        private async Task<PaymentResponse> ProcessPaymentExternalAsync(decimal amount)
        {
            await Task.Delay(1000); // Simulate network call to payment gateway
            return new PaymentResponse { Success = true };
        }

        // Flaw 7: 'async void' should NEVER be used except for UI event handlers. It crashes the application if it fails.
        private async void NotifyUserAsync(int userId, int invoiceId)
        {
            await Task.Delay(500); // Simulate email sending
            LogMessage($"User {userId} notified for invoice {invoiceId}");
        }

        private void LogMessage(string message)
        {
            // Flaw 8: Hardcoded file writing instead of using an ILogger interface
            System.IO.File.AppendAllText("payment_logs.txt", $"{DateTime.Now}: {message}\n");
        }
    }

    // --- Dummy classes below just to make the code valid ---
    public class DatabaseContext { 
        public List<User> Users { get; set; } = new List<User>(); 
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public Task SaveChangesAsync() => Task.CompletedTask;
    }
    public class User { public int Id { get; set; } public string Email { get; set; } }
    public class Invoice { public int Id { get; set; } public int UserId { get; set; } public decimal Amount { get; set; } public bool IsPaid { get; set; } }
    public class PaymentResponse { public bool Success { get; set; } }
}
